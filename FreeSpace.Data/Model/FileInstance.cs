using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSpace.Data.Model
{
    public class FileInstance
    {
        public int Id { get; set; }
        public int FileThumbPrintId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        //public string MD5 { get; set; }
        //public long FileSize { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public DateTime CreatedStamp{ get; set; }
        public DateTime UpdatedStamp { get; set; }
        public bool IsHistory { get; set; }
    }
}
