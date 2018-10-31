using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSpace.Lib
{
    public class FileIdentity
    {
        public string FileExt { get; set; }
        public string MD5 { get; set; }
        public long FileSize { get; set; }
        public List<FileInstance> FileInstances { get; private set; }

        public FileIdentity(FileInfo file)
        {
            this.FileExt = file.Extension;
            this.MD5 = Utility.CalculateMD5(file.FullName);
            this.FileSize = file.Length;
            FileInstances = new List<FileInstance>()
            {
                new FileInstance()
                {
                    FileName = file.Name,
                    FilePath = file.DirectoryName,
                    Created = file.CreationTime,
                    Modified = file.LastWriteTime
                }
            };
        }

        public void AddInstance(FileInstance inst)
        {
            FileInstances.Add(inst);
        }
    }
}
