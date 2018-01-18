using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace Yuan.Device.Mouse
{
    public class VirtualMouse
    {
        public VirtualMouse()
        {

        }
        private const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        private const uint MOUSEEVENTF_HWHEEL = 0x01000;
        private const uint MOUSEEVENTF_MOVE = 0x0001;
        private const uint MOUSEEVENTF_MOVE_NOCOALESCE = 0x2000;
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        private const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const uint MOUSEEVENTF_VIRTUALDESK = 0x4000;
        private const uint MOUSEEVENTF_WHEEL = 0x0800;
        private const uint MOUSEEVENTF_XDOWN = 0x0080;
        private const uint MOUSEEVENTF_XUP = 0x0100;
        /// <summary>
        /// 以自然方式移動滑鼠。
        /// </summary>
        /// <param name="from">開始的點</param>
        /// <param name="to">結束點</param>
        /// <param name="Sec">移動所花費時間(以毫秒為單位)</param>
        public void MoveMouse(Point from, Point to,int ms)
        {
            int MoveX = to.X - from.X;
            int MoveY = to.Y - from.Y;
            double MoveX_ms = (double)MoveX / (double)ms;
            double MoveY_ms = (double)MoveY / (double)ms;
            DateTime dt = DateTime.Now;
            TimeSpan a = DateTime.Now - dt;
            int X = from.X;
            int Y = from.Y;
            Mouse mouse = new Mouse();
            mouse.Location = new Point(X, Y);
            while (a.TotalMilliseconds <= ms)
            {

                mouse.Location = new Point((int)System.Math.Round(X + (MoveX_ms * a.TotalMilliseconds)), (int)(Y + (MoveY_ms * a.TotalMilliseconds)));
               a = DateTime.Now - dt;
                
            }
        }
        
        /// <summary>
        /// 按下滑鼠左鍵
        /// </summary>
        public void ClickMouse()
        {
            Mouse mouse = new Mouse();
            mouse_event(WindowsAPI.User32.MOUSEEVENTF_LEFTDOWN, mouse.Location.X, mouse.Location.Y, WindowsAPI.User32.XBUTTON2, WindowsAPI.User32.GetMessageExtraInfo());
           // System.Threading.Thread.Sleep(10);
            mouse_event(WindowsAPI.User32.MOUSEEVENTF_LEFTUP, mouse.Location.X, mouse.Location.Y, WindowsAPI.User32.XBUTTON2, WindowsAPI.User32.GetMessageExtraInfo());

        }
        public void ClickMouse(int x,int y)
        {
            Mouse mouse = new Mouse();
            mouse_event(WindowsAPI.User32.MOUSEEVENTF_LEFTDOWN, x, y, WindowsAPI.User32.XBUTTON2, WindowsAPI.User32.GetMessageExtraInfo());
            // System.Threading.Thread.Sleep(10);
            mouse_event(WindowsAPI.User32.MOUSEEVENTF_LEFTUP, x, y, WindowsAPI.User32.XBUTTON2, WindowsAPI.User32.GetMessageExtraInfo());

        }
        [DllImport("User32.dll", SetLastError = true)]
        private extern static void mouse_event(uint dwFlags, int dx, int dy, uint dwData, IntPtr dwExtraInfo);
    }
}
