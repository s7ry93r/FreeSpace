using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeSpace.Data;
using FreeSpace.Data.Model;
using System.IO;
using FreeSpace.Data.Repo;

namespace FreeSpace.Lib
{


    public class FileInspection
    {
        protected Repo repo;


        public FileInspection()
        {
           repo = new Repo(); 
        }

        public virtual ScanResult ScanAndRecord(FileInfo file)
        {
            var scanResult = new ScanResult();
            //var file = new FileInfo(fullPath);
            var instance = GetFileInstance(file);
            scanResult.IsBig = file.Length > (5 * Utility.Megabyte);
            scanResult.IsPossibleDupe = repo.RetrieveAllInstancesWithThumbPrint(instance.FileThumbPrintId).Count > 1;
            scanResult.IsFileSystemLocked = IsFileLocked(file);
            return scanResult;
        }

        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                stream?.Close();
            }

            //file is not locked
            return false;
        }

        protected virtual FileInstance GetFileInstance( FileInfo file)
        {

            var existing = repo.RetrieveFileInstance(file.FullName);
            if (existing != null)
            {
                var tp = repo.RetrieveFileThumbPrint(existing.FileThumbPrintId);
                var md5 = Utility.CalculateMD5(file.FullName);
                if (tp.FileSize != file.Length || tp.MD5 != md5)
                {
                    repo.SoftDeleteFileThumbPrint(tp.Id);
                    var newTP = GetFileThumbPrint(file);
                    existing.FileThumbPrintId = newTP.Id;
                    existing = repo.UpdateFileInstance(existing);
                }

                return existing;
            }
            else
            {
                var tp = GetFileThumbPrint(file);
                var newFI = new FileInstance()
                {
                    FileThumbPrintId = tp.Id,
                    FileName = file.Name,
                    FilePath = file.DirectoryName,
                    Created = file.CreationTime,
                    Modified = file.LastWriteTime,
                };
                repo.CreateFileInstance(newFI);

                return newFI;
            }
        }

        protected virtual FileThumbPrint GetFileThumbPrint(FileInfo file)
        {
            var md5 = Utility.CalculateMD5(file.FullName);
            var existing = repo.RetrieveFileThumbPrint(md5, file.Length);
            if (existing != null)
            {
                existing = repo.UpdateFileThumbPrint(existing);
                return existing;
            }
            else
            {
                var newTP = new FileThumbPrint()
                {
                    MD5 = md5,
                    FileSize = file.Length,
                };
                newTP = repo.CreateFileThumbPrint(newTP);
                return newTP;
            }
        }


    }
}
