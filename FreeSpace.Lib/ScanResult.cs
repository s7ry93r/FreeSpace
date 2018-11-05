using System;

namespace FreeSpace.Lib
{
    public class ScanResult
    {
        public Boolean IsFileSystemLocked { get; set; }
        public Boolean IsPossibleDupe { get; set; }
        public Boolean IsBig { get; set; }
    }
}