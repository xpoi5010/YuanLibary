using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
namespace Yuan.Device.Keyboard
{

    /// <summary>
    /// 用於模擬鍵盤。
    /// </summary>
    public class VirtualKeyboard
    {

        /// <summary>
        /// 初始化。
        /// </summary>
        public VirtualKeyboard()
        {

        }
        [DllImport("User32.dll")]
        private extern static void keybd_event(byte bVk, byte bScan, int dwFlags, IntPtr dwExtraInfo);

        /// <summary>
        /// 用於發送一個虛擬按鍵。
        /// </summary>
        /// <param name="SendCode">傳送的按鍵代碼。</param>
        /// <param name="KeyStatus">傳送的按鍵動作。</param>
        /// <returns>傳回該函數是否呼叫成功。</returns>
        public bool SendKey(KeyCodes SendCode,KeyStatus KeyStatus)
        {
            try
            {
                keybd_event((byte)SendCode, 0, (int)KeyStatus, (IntPtr)0);
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        /// <summary>
        /// 用於發送一個虛擬按鍵。
        /// </summary>
        /// <param name="SendCode">要傳送"按下"指令的按鍵代碼</param>
        /// <returns></returns>
        public bool PressKey(KeyCodes SendCode)
        {
            bool a = SendKey(SendCode,KeyStatus.KeyDown);
            Thread.Sleep(1);
            bool b = SendKey(SendCode, KeyStatus.KeyUP);
            return a || b;
        }
        
    }
}
