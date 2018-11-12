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

        public virtual ScanResult ScanAndRecord(FileInfo fileInfo)
        {
            var scanResult = new ScanResult();
            scanResult.IsFileSystemLocked = Utility.IsFileLocked(fileInfo);
            scanResult.MD5 = Utility.CalculateMD5(fileInfo);
            scanResult.IsBig = fileInfo.Length > (5 * Utility.Megabyte);

            var instance = GetFileInstance(fileInfo, scanResult);
            scanResult.IsPossibleDupe = repo.RetrieveAllInstancesWithThumbPrint(instance.FileThumbPrintId).Count > 1;

            return scanResult;
        }


        protected virtual FileInstance GetFileInstance(FileInfo fileInfo, ScanResult scanResult)
        {
            var existing = repo.RetrieveFileInstance(fileInfo);
            if (existing != null)
            {
                var tp = repo.RetrieveFileThumbPrint(existing.FileThumbPrintId);
                if (tp.FileSize != fileInfo.Length || tp.MD5 != scanResult.MD5)
                {
                    repo.SoftDeleteFileThumbPrint(tp.Id);
                    var newTP = GetFileThumbPrint(fileInfo, scanResult);
                    existing.FileThumbPrintId = newTP.Id;
                    existing = repo.UpdateFileInstance(existing);
                }

                return existing;
            }
            else
            {
                var tp = GetFileThumbPrint(fileInfo, scanResult);
                var newFI = new FileInstance()
                {
                    FileThumbPrintId = tp.Id,
                    FileName = fileInfo.Name,
                    FilePath = fileInfo.DirectoryName,
                    Created = fileInfo.CreationTime,
                    Modified = fileInfo.LastWriteTime,
                };
                repo.CreateFileInstance(newFI);

                return newFI;
            }
        }

        protected virtual FileThumbPrint GetFileThumbPrint(FileInfo fileInfo, ScanResult scanResult)
        {
            var existing = repo.RetrieveFileThumbPrint(scanResult.MD5, fileInfo.Length);
            if (existing != null)
            {
                existing = repo.UpdateFileThumbPrint(existing);
                return existing;
            }
            else
            {
                var newTP = new FileThumbPrint()
                {
                    MD5 = scanResult.MD5,
                    FileSize = fileInfo.Length,
                };
                newTP = repo.CreateFileThumbPrint(newTP);
                return newTP;
            }
        }


    }
}
