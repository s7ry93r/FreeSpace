using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSpace.Data.Model
{
    public class FileThumbPrint
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string MD5 { get; set; }

        public long FileSize { get; set; }
        public DateTime CreatedStamp { get; set; }
        public DateTime UpdatedStamp { get; set; }
        public bool IsHistory { get; set; }
    }
}
