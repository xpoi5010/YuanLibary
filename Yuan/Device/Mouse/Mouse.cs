using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
namespace Yuan.Device.Mouse
{
    public class Mouse
    {
        public Mouse()
        {

        }
        [DllImport("User32.dll")]
        private extern static bool GetCursorPos(out Point LPPOINT);
        [DllImport("User32.dll")]
        private extern static bool SetCursorPos(int x,int y);
        public Point Location
        {
            get
            {
                Point a = new Point();
                GetCursorPos(out a);
                return a;
            }
            set
            {
                SetCursorPos(value.X, value.Y);
            }
        }
    }
}
