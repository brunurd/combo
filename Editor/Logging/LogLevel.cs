using System;

namespace Combo.Editor.Logging
{
    [Flags]
    public enum LogLevel
    {
        None = 0,
        Log = 1,
        Warning = 2,
        Error = 4
    }
}