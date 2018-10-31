using System.Collections.Generic;
using System.IO;

namespace FreeSpace.Lib
{
    public class FileManager
    {
        public List<FileIdentity> FileIdentities { get; private set; }

        public void AddFile(FileInfo fi)
        {
            var hold = new FileIdentity(fi);
            var ident = matchedTo(hold);
            if (ident == null)
            {
                FileIdentities.Add(hold);
            }
            else
            {
                ident.AddInstance(hold.FileInstances[0]);
            }
        }

        private FileIdentity matchedTo(FileIdentity hold)
        {
            foreach (var ident in FileIdentities)
            {
                if (ident.FileExt.ToUpper() == hold.FileExt.ToUpper() &&
                    ident.FileSize == hold.FileSize &&
                    ident.MD5 == hold.MD5)
                {
                    return ident;
                }
            }

            return null;
        }
    }
}