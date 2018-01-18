using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
namespace Yuan.Device.Mouse.Hook
{
    public class MouseHook
    {
        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, int dwThreadId);
        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetModuleHandle(string lpFileName);
        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr CallNextHookEx(int hhk, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);
        public int Hook;
        private static LowLevelMouseProc ps;
        public MouseHook()
        {
            ps = new LowLevelMouseProc(process);
            using(Process ps_ = Process.GetCurrentProcess())
            {
                using(ProcessModule pm = ps_.MainModule)
                {
                    Hook = SetWindowsHookEx(14, ps, GetModuleHandle(pm.ModuleName), 0);//設置一個低級的鉤子
                    if (Hook == 0)
                        throw new Exception("Hook設置失敗。");
                }
            }
        }
        public void UnHook()
        {
            if(Hook!=0)
                UnhookWindowsHookEx(Hook);

        }
        ~MouseHook()
        {
            UnHook();
        }
        public delegate void LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        private void process(int nCode, IntPtr wParam, IntPtr lParam)
        {
            /* lParam 
             * pt POINT LONG LONG 32bit 4byte*2 offset:0~7
             * mouseData DWORD 32BIT 4byte offset:8~11
             * flags DWORD 32bit 4byte offset:12~15
             * time DWORD 32bit 4byte offset 16~19
             * WParam
             * 
            */
            int x = Marshal.ReadInt32(lParam,0);
            int y = Marshal.ReadInt32(lParam, 4);
            System.Drawing.Point point = new System.Drawing.Point(x, y);
            MouseEvent me = (MouseEvent)wParam.ToInt32();
            if(me== MouseEvent.MouseMove)
            {
                MouseMove?.Invoke(this, new MouseMoveEventArgs(point));
            }
            else if(me==MouseEvent.MouseRightButtonDown|| me == MouseEvent.MouseLeftButtonDown|| me == MouseEvent.MiddleButtonDown)
            {
                MouseKey mouseKey =MouseKey.LeftButton;
                switch (me)
                {
                    case (MouseEvent.MouseRightButtonDown):
                        mouseKey = MouseKey.RightButton;
                        break;
                    case (MouseEvent.MouseLeftButtonDown):
                        mouseKey = MouseKey.LeftButton;
                        break;
                    case (MouseEvent.MiddleButtonDown):
                        mouseKey = MouseKey.MiddleButton;
                        break;
                }
                
                MouseKeyDown?.Invoke(this, new MouseKeyEventArgs(mouseKey, point));
            }
            else if(me == MouseEvent.MouseRightButtonUp || me == MouseEvent.MouseLeftButtonUp || me == MouseEvent.MiddleButtonUp)
            {
                MouseKey mouseKey = MouseKey.LeftButton;
                switch (me)
                {
                    case (MouseEvent.MouseRightButtonUp):
                        mouseKey = MouseKey.RightButton;
                        break;
                    case (MouseEvent.MouseLeftButtonUp):
                        mouseKey = MouseKey.LeftButton;
                        break;
                    case (MouseEvent.MiddleButtonUp):
                        mouseKey = MouseKey.MiddleButton;
                        break;
                }

                MouseKeyUp?.Invoke(this, new MouseKeyEventArgs(mouseKey, point));
            }
            else if(me== MouseEvent.MouseWhell)
            {
                MouseWhell?.Invoke(this, new MouseWhellEventArgs(point));
            }
            Debug.Print(((MouseEvent)wParam.ToInt32()).ToString());
            CallNextHookEx(Hook, nCode, wParam, lParam);
        }
        public delegate void mousemoveevent(object sender, MouseMoveEventArgs e);
        public event mousemoveevent MouseMove;
        public delegate void mousekeyevent(object sender, MouseKeyEventArgs e);
        public delegate void mousewhellevent(object sender, MouseWhellEventArgs e);
        public event mousekeyevent MouseKeyUp;
        public event mousekeyevent MouseKeyDown;
        public event mousewhellevent MouseWhell;
        private enum MouseEvent
        {
            MouseMove = 0x0200, MouseLeftButtonDown = 0x0201, MouseLeftButtonUp = 0x0202, MouseRightButtonDown = 0x0204, MouseRightButtonUp = 0x0205, MiddleButtonDown = 0x0207, MiddleButtonUp = 0x0208,MouseWhell=0x20A
        }
        public enum MouseKey
        {
            LeftButton,RightButton,MiddleButton
        }
        public class MouseKeyEventArgs
        {
            public MouseKey Key
            {
                get;
            }
            public System.Drawing.Point Location { get; }
            public MouseKeyEventArgs(MouseKey key, System.Drawing.Point MouseLocation)
            {
                Key = key;
                Location = MouseLocation;
            }
        }
        public class MouseMoveEventArgs
        {
            public System.Drawing.Point Location { get; }
            public MouseMoveEventArgs(System.Drawing.Point MouseLocation)
            {
                Location = MouseLocation;
            }
        }
        public class MouseWhellEventArgs
        {
            public System.Drawing.Point Location { get; }
            public MouseWhellEventArgs(System.Drawing.Point MouseLocation)
            {
                Location = MouseLocation;
            }
        }
    }
}
