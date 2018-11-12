using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FreeSpace.Data.Model;
using FreeSpace.Data.Repo;

namespace FreeSpace.Lib
{
    public class Utility
    {
        public static long Kilobyte = 1024;
        public static long Megabyte = 1048576;
        public static long Gigabyte = 1073741824;

        public static string CalculateMD5(FileInfo fileInfo)
        {
            using (var md5 = MD5.Create())
            {
                if (!IsFileLocked(fileInfo))
                {
                    using (var stream = File.OpenRead(fileInfo.FullName))
                    {
                        var hash = md5.ComputeHash(stream);
                        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                }
                else
                {
                    return "CantGetMd5CuzFileOpen";
                }
            }
        }

        public static bool IsDirectoryLocked(string dirPath)
        {
            try
            {
                var dirInfo = new DirectoryInfo(dirPath);
                var files = dirInfo.GetFiles();
                return false;
            }
            catch (UnauthorizedAccessException uae)
            {
                CreateScanException(uae, ScanExceptionType.Directory | ScanExceptionType.Security, "Unauthorized Directory", dirPath);
                return true;
            }
            catch (DirectoryNotFoundException dnf)
            {
                CreateScanException(dnf, ScanExceptionType.Directory | ScanExceptionType.NotFound, "Directory Not Found", dirPath);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (UnauthorizedAccessException uae)
            {
                CreateScanException(uae, ScanExceptionType.File | ScanExceptionType.Security, "Unauthorized file", file.FullName);
                return true;
            }
            catch (IOException ioe)
            {
                CreateScanException(ioe, ScanExceptionType.File | ScanExceptionType.InUse, "File In Use", file.FullName);
                return true;
            }
            finally
            {
                stream?.Close();
            }

            //file is not locked
            return false;
        }

        public static void CreateScanException(Exception ex, ScanExceptionType scanExType, string shortDesc, string fullPath)
        {
            var repo = new Repo();
            repo.CreateScanException(ex, scanExType, shortDesc, fullPath);
        }
    }
}
