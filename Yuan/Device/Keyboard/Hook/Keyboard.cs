using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;
namespace Yuan.Device.Keyboard.Hook
{
    public class KeyboardHook
    {
        [DllImport("User32.dll", SetLastError = true,CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook,LowLevelKeyboardProc lpfn, IntPtr hMod, int dwThreadId);
        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetModuleHandle(string lpFileName);
        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern short GetKeyState(int nVirtKey);
        [DllImport("User32.dll",SetLastError =true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr CallNextHookEx(int hhk ,int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);
        public int Hook;
        private static LowLevelKeyboardProc ps;
        public KeyboardHook()
        {
            ps = new LowLevelKeyboardProc(process);
            using (Process process_ = Process.GetCurrentProcess())
            {

                using(ProcessModule processModule = process_.MainModule)
                {
                    Hook = SetWindowsHookEx(13,  ps, GetModuleHandle(processModule.ModuleName), 0);//設置一個鉤子，低級
                    GC.Collect();
                    
                    if (Hook == 0) throw new System.Exception("");
                }
            }
        }
        public void Unlock()
        {
            UnhookWindowsHookEx(Hook);
        }
        public class KeyboardEvents
        {
            public KeyCodes Key
            {
                get;
                private set;
                
            }
            public KeyboardEvents(KeyCodes key)
            {
                
                Key = key;
            }
        }
        public delegate void LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        public void process(int nCode,IntPtr wParam, IntPtr lParam)
        {
            //if (ps == null) ps = new LowLevelKeyboardProc(process);
            
            /*
            int Keycode = wParam.ToInt32();
            short Action = GetKeyState(Keycode);
            int low = Action & 1;
            int high = Action >> 15;
            System.Diagnostics.Debug.Print(Action.ToString());
            if (high == 0)
            {
                KeyUP?.Invoke(this, new KeyboardEvents((KeyCodes)Keycode, KeyStatus.KeyUP));
            }
            else
            {

                KeyDown?.Invoke(this, new KeyboardEvents((KeyCodes)Keycode, KeyStatus.KeyDown));
            }
            */
            //LL
            
            int Keycode = Marshal.ReadInt32(lParam,0);
            int saneCode = Marshal.ReadInt32(lParam, 4);
            int flags = Marshal.ReadInt32(lParam, 8);
            int Action = flags;
            //Debug.Print("flags:{0}",flags.ToString());
            int low = Action & 1;
            int high = Action >> 7;
            //Debug.Print("Action:{0}", Action.ToString());
            if (high == 0)
            {
                KeyDown?.Invoke(this, new KeyboardEvents((KeyCodes)Keycode));
            }
            else
            {

                KeyUP?.Invoke(this, new KeyboardEvents((KeyCodes)Keycode));
            }
            CallNextHookEx(Hook,nCode, wParam, lParam);
        }
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        public const int WM_SYSKEYDOWN = 0x0104;
        public const int WM_SYSKEYUP = 0x0105;
        public struct KBDLLHOOKSTRUCT
        {
            public uint vkCode;
            public uint scanCode;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        public delegate void KeyEvents(object sender, KeyboardEvents e);
        public event KeyEvents KeyUP;
        public event KeyEvents KeyDown;
        ~KeyboardHook()
        {
            Unlock();
        }
    }
}
