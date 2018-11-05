using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FreeSpace.Data.Model;

namespace FreeSpace.Data.Repo
{
    public class Repo
    {

        public FileThumbPrint RetrieveFileThumbPrint(string md5, long fileSize)
        {
            using (var ctx = new FreeSpaceContext())
            {
                return ctx.FileThumbPrints.FirstOrDefault(tp => tp.MD5 == md5 && tp.FileSize == fileSize && !tp.IsHistory);
            }
        }

        public FileThumbPrint RetrieveFileThumbPrint(int Id)
        {
            using (var ctx = new FreeSpaceContext())
            {
                return ctx.FileThumbPrints.Find(Id);
            }
        }

        public FileThumbPrint CreateFileThumbPrint(FileThumbPrint thumbPrint)
        {
            using (var ctx = new FreeSpaceContext())
            {
                thumbPrint.CreatedStamp = DateTime.Now;
                thumbPrint.UpdatedStamp = DateTime.Now;
                ctx.FileThumbPrints.Add(thumbPrint);
                ctx.SaveChanges();
                return thumbPrint;
            }
        }

        public FileThumbPrint UpdateFileThumbPrint(FileThumbPrint thumbPrint)
        {
            using (var ctx = new FreeSpaceContext())
            {
                thumbPrint.UpdatedStamp = DateTime.Now;
                ctx.FileThumbPrints.Attach(thumbPrint);
                ctx.SaveChanges();
                return thumbPrint;
            }
        }

        public void SoftDeleteFileThumbPrint(int Id)
        {
            var list = RetrieveAllInstancesWithThumbPrint(Id);
            if (list.Count == 1)
            {
                using (var ctx = new FreeSpaceContext())
                {
                    var tp = ctx.FileThumbPrints.Find(Id);
                    tp.IsHistory = true;
                    tp.UpdatedStamp = DateTime.Now;
                    ctx.FileThumbPrints.Attach(tp);
                    ctx.SaveChanges();
                }
            }
        }

        public List<FileInstance> RetrieveAllInstancesWithThumbPrint(int Id)
        {
            using (var ctx = new FreeSpaceContext())
            {
                return (from f in ctx.FileInstances where f.FileThumbPrintId == Id && !f.IsHistory select f).ToList();
            }
        }



        public FileInstance CreateFileInstance(FileInstance instance)
        {
            using (var ctx = new FreeSpaceContext())
            {
                instance.CreatedStamp = DateTime.Now;
                instance.UpdatedStamp = DateTime.Now;
                ctx.FileInstances.Add(instance);
                ctx.SaveChanges();
                return instance;
            }
        }

        public FileInstance RetrieveFileInstance(string fullPath)
        {
            var fi = new FileInfo(fullPath);
            using (var ctx = new FreeSpaceContext())
            {
                return ctx.FileInstances.FirstOrDefault(f => f.FilePath == fi.DirectoryName && f.FileName == fi.Name && !f.IsHistory);
            }
        }

        public FileInstance UpdateFileInstance(FileInstance instance)
        {
            using (var ctx = new FreeSpaceContext())
            {
                instance.UpdatedStamp = DateTime.Now;
                ctx.FileInstances.Attach(instance);
                ctx.SaveChanges();
                return instance;
            }
        }



    }
}
