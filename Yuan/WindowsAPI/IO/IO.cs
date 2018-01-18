using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace Yuan.WindowsAPI
{
    public static class IO
    {
        [DllImport("Kernel32.dll")]
        public extern static IntPtr CreateFileW(string lpFileName, int dwDesiredAccess, int dwShareMode,int dwCreationDisposition,int dwFlagsAndAttributes,IntPtr hTemplateFile);
        public struct _PRIVILEGE_SET
        {
            int PrivilegeCount;
            int Control;
            _LUID_AND_ATTRIBUTES[] Privilege;
        }
        public struct _LUID_AND_ATTRIBUTES
        {
            _LUID Luid;
            int Attributes;
        }
        public struct _LUID
        {
            int LowPart;
            long HighPart;
        }
        [DllImport("Kernel32.dll")]
        public extern static IntPtr ReadDirectoryChangesW(IntPtr hDirectory, IntPtr lpBuffer, int nBufferLength, bool bWatchSubtree, int dwNotifyFilter, IntPtr lpBytesReturned);
    }
    
}
