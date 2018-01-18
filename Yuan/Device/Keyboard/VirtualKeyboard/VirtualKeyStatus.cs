using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuan.Device.Keyboard
{
    public enum KeyStatus
    {
        KeyDown = 0x01|0,
        KeyUP = 0x01|0x02
    }
}
