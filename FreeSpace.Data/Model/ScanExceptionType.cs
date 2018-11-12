using System;

namespace FreeSpace.Data.Model
{
    [Flags]
    public enum ScanExceptionType
    {
        File = 1,
        Directory = 2,
        NotFound = 4,
        InUse = 8,
        Security = 16,
        PathProblem = 32,
    }
}