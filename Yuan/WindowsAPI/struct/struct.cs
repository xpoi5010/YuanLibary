using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yuan.WindowsAPI.Struct
{
    public struct tagKBDLLHOOKSTRUCT
    {
        int vkCode;
        int scanCode;
        int flags;
        int time;
        IntPtr dwExtraInfo;
    }
}
