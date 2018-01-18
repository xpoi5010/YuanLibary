using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
namespace Yuan.WindowsAPI
{
    public class System
    {
        [DllImport("Kernel32.dll")]
        public extern static IntPtr GetSystemInfo(out LPSYSTEM_INFO lpSystemInfo);
        public struct LPSYSTEM_INFO
        { 
            int dwOemId;
            Int16 wProcessorArchitecture;
            Int16 wReserved;
            int dwPageSize;
            IntPtr lpMinimumApplicationAddress;
            IntPtr lpMaximumApplicationAddress;
            int dwActiveProcessorMask;
            int dwNumberOfProcessors;
            int dwProcessorType;
            int dwAllocationGranularity;
            Int16 wProcessorLevel;
            Int16 wProcessorRevision;
        }
        [DllImport("User32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, Delegate lpfn, IntPtr hMod, int dwThreadId);
        [DllImport("Kernel32.dll")]
        public static extern IntPtr LoadLibrary(string lpFileName);
    }
}
