using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSpace.Data.Model
{
    public class FileInstance
    {
        public int Id { get; set; }
        public int FileThumbPrintId { get; set; }

        [MaxLength(512)]
        public string FilePath { get; set; }

        [MaxLength(256)]
        public string FileName { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public DateTime CreatedStamp{ get; set; }
        public DateTime UpdatedStamp { get; set; }
        public bool IsHistory { get; set; }
    }
}
