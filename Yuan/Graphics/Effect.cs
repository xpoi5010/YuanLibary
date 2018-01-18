using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;
using System.Resources;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Yuan
{
    public static class Const
    {
        public static Guid BlurEffectGuid = new Guid("633C80A4-1843-482b-9EF2-BE2834C5FDD4");
    }
    public static class Function
    {
        const string dllname = "Gdiplus.dll";
        [DllImport(dllname, CharSet = CharSet.Unicode, SetLastError = true)]
        public extern static int GdipCreateEffect(Guid guid, out IntPtr effect);
        [DllImport(dllname, CharSet = CharSet.Unicode, SetLastError = true)]
        public extern static int GdipDeleteEffect(IntPtr effect);
        [DllImport(dllname, CharSet = CharSet.Unicode, SetLastError = true)]
        public extern static int GdipGetEffectParameterSize(IntPtr effect, uint size);
        //Status __stdcall GdipSetEffectParameters(CGpEffect *effect, const VOID *params, const UINT size)
        [DllImport(dllname, CharSet = CharSet.Unicode, SetLastError = true)]
        public extern static int GdipSetEffectParameters(IntPtr effect, IntPtr _params, uint size);
        //Status __stdcall GdipGetEffectParameters(CGpEffect *effect, UINT *size, VOID *params)
        [DllImport(dllname, CharSet = CharSet.Unicode, SetLastError = true)]
        public extern static int GdipGetEffectParameters(IntPtr effect, uint size, IntPtr _params);
        //GdipBitmapApplyEffect(GpBitmap* bitmap, CGpEffect *effect, RECT *roi, BOOL useAuxData, VOID **auxData, INT *auxDataSize)
        [DllImport(dllname, CharSet = CharSet.Unicode, SetLastError = true)]
        public extern static int GdipBitmapApplyEffect(IntPtr bitmap, IntPtr effect, ref System.Drawing.Rectangle roi, bool useAuxData, IntPtr auxData, int auxDataSize);
    }//
    public class Effect
    {
        protected IntPtr auxData;
        protected int auxDataSize;
        protected bool useAuxData;
        public IntPtr nativeEffect;
        public Effect()
        {
            auxDataSize = 0;
            auxData = IntPtr.Zero;
            nativeEffect = IntPtr.Zero;
            useAuxData = false;
        }
        public int GetAuxDataSize()
        {
            return auxDataSize;
        }
        public object GetAuxData()
        {
            return auxData;
        }
        public void UseAuxData(bool useAuxDataFlag)
        {
            useAuxData = useAuxDataFlag;
        }
        public int GetParamerterSize(uint size)
        {
            return Function.GdipGetEffectParameterSize(nativeEffect, size);
        }
        //protected
        protected int SetParameters(IntPtr _params, uint size)
        {
            return Function.GdipSetEffectParameters(nativeEffect, _params, size);
        }
        protected int GetParameters(uint size, IntPtr _params)
        {
            return Function.GdipGetEffectParameters(nativeEffect, size, _params);
        }
    }
    
    public class Blur : Effect
    {
        public Blur() : base()
        {
            Function.GdipCreateEffect(Const.BlurEffectGuid, out nativeEffect);
        }
        public int SetParameters(BlurParams parameters)
        {
            IntPtr temp = Marshal.AllocHGlobal(Marshal.SizeOf(parameters));
            return Function.GdipSetEffectParameters(nativeEffect, temp, (uint)Marshal.SizeOf(parameters));
        }
        public int GetParameters(uint size, BlurParams parameters)
        {
            IntPtr temp = Marshal.AllocHGlobal(Marshal.SizeOf(parameters));
            return Function.GdipGetEffectParameters(nativeEffect, (uint)Marshal.SizeOf(parameters), temp);
        }
    }
    public struct SharpenParams
    {
        public float radius;
        public float amount;
    }
    public struct BlurParams
    {
        public float radius;
        public bool expandEdge;
    }
    public struct BrightnessContrastParams
    {
        public int brightnessLevel;
        public int contrastLevel;
    }


    public struct HueSaturationLightnessParams
    {
        public int hueLevel;
        public int saturationLevel;
        public int lightnessLevel;
    }

    public struct TintParams
    {
        public int hue;
        public int amount;
    }

    public struct LevelsParams
    {
        public int highlight;
        public int midtone;
        public int shadow;
    }

    public struct ColorBalanceParams
    {
        public int cyanRed;
        public int magentaGreen;
        public int yellowBlue;
    }
    public struct GUID
    {
        public uint Data1;
        public UInt16 Data2;
        public UInt16 Data3;
        public byte[] Data4;
    }
}