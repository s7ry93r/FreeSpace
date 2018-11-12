using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSpace.Data.Model
{
    public class ScanException
    {
        public int Id { get; set; }

        //int value of ScanExptionType 
        public int ExceptionType { get; set; }

        [MaxLength(1024)]
        public string Path { get; set; }

        [MaxLength(250)]
        public string ShortDesc { get; set; }
        public string ExceptionName { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public int HResult { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public DateTime CreatedStamp { get; set; }
    }
}
