using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
namespace Yuan.WindowsAPI
{
    public unsafe static class User32
    {
        //Const
        public const string dllName = "User32.dll";
           //WindowsStyle
        public const uint WS_BORDER = 0x00800000;
        public const uint WS_CAPTION = 0x00C00000;
        public const uint WS_CHILD = 0x40000000;
        public const uint WS_CHILDWINDOW = 0x40000000;
        public const uint WS_CLIPCHILDREN = 0x02000000;
        public const uint WS_CLIPSIBLINGS = 0x04000000;
        public const uint WS_DISABLED = 0x08000000;
        public const uint WS_DLGFRAME = 0x00400000;
        public const uint WS_GROUP = 0x00020000;
        public const uint WS_HSCROLL = 0x00100000;
        public const uint WS_ICONIC = 0x20000000;
        public const uint WS_MAXIMIZE = 0x01000000;
        public const uint WS_MAXIMIZEBOX = 0x00010000;
        public const uint WS_MINIMIZE = 0x20000000;
        public const uint WS_MINIMIZEBOX = 0x00020000;
        public const uint WS_OVERLAPPED = 0x00000000;
        public const uint WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX);
        public const uint WS_POPUP = 0x80000000;
        public const uint WS_POPUPWINDOW = (WS_POPUP | WS_BORDER | WS_SYSMENU);
        public const uint WS_SIZEBOX = 0x00040000;
        public const uint WS_SYSMENU = 0x00080000;
        public const uint WS_TABSTOP = 0x00010000;
        public const uint WS_THICKFRAME = 0x00040000;
        public const uint WS_TILED = 0x00000000;
        public const uint WS_TILEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX);
        public const uint WS_VISIBLE = 0x10000000;
        public const uint WS_VSCROLL = 0x00200000;
           //ExtendedWindowStyles
        public const uint WS_EX_ACCEPTFILES = 0x00000010;
        public const uint WS_EX_APPWINDOW = 0x00040000;
        public const uint WS_EX_CLIENTEDGE = 0x00000200;
        public const uint WS_EX_COMPOSITED = 0x02000000;
        public const uint WS_EX_CONTEXTHELP = 0x00000400;
        public const uint WS_EX_CONTROLPARENT = 0x00010000;
        public const uint WS_EX_DLGMODALFRAME = 0x00000001;
        public const uint WS_EX_LAYERED = 0x00080000;
        public const uint WS_EX_LAYOUTRTL = 0x00400000;
        public const uint WS_EX_LEFT = 0x00000000;
        public const uint WS_EX_LEFTSCROLLBAR = 0x00004000;
        public const uint WS_EX_LTRREADING = 0x00000000;
        public const uint WS_EX_MDICHILD = 0x00000040;
        public const uint WS_EX_NOACTIVATE = 0x08000000;
        public const uint WS_EX_NOINHERITLAYOUT = 0x00100000;
        public const uint WS_EX_NOPARENTNOTIFY = 0x00000004;
        public const uint WS_EX_NOREDIRECTIONBITMAP = 0x00200000;
        public const uint WS_EX_RIGHT = 0x00001000;
        public const uint WS_EX_RIGHTSCROLLBAR = 0x00000000;
        public const uint WS_EX_RTLREADING = 0x00002000;
        public const uint WS_EX_STATICEDGE = 0x00020000;
        public const uint WS_EX_TOOLWINDOW = 0x00000080;
        public const uint WS_EX_TOPMOST = 0x00000008;
        public const uint WS_EX_TRANSPARENT = 0x00000020;
        public const uint WS_EX_WINDOWEDGE = 0x00000100;
        //ChangeWindowMessageFilterEx Action
        public const uint MSGFLT_ALLOW = 1;
        public const uint MSGFLT_DISALLOW = 2;
        public const uint MSGFLT_RESET = 0;
        //ChangeWindowMessageFilter dwFlag
        public const uint MSGFLT_ADD = 1;
        public const uint MSGFLT_REMOVE = 2;
        //CascadeWindows wHow 
        public const uint MDITILE_SKIPDISABLED = 0x0002;
        public const uint MDITILE_ZORDER = 0x0004;
        //CalculatePopupWindowPosition flags
        public const uint TPM_CENTERALIGN = 0x0004;
        public const uint TPM_LEFTALIGN = 0x0000;
        public const uint TPM_RIGHTALIGN = 0x0008;
        //AnimateWindow dwFlags
        public const uint AW_ACTIVATE = 0x00020000;
        public const uint AW_BLEND = 0x00080000;
        public const uint AW_CENTER = 0x00000010;
        public const uint AW_HIDE = 0x00010000;
        public const uint AW_HOR_POSITIVE = 0x00000001;
        public const uint AW_HOR_NEGATIVE = 0x00000002;
        public const uint AW_SLIDE = 0x00040000;
        public const uint AW_VER_POSITIVE = 0x00000004;
        public const uint AW_VER_NEGATIVE = 0x00000008;
        //ChildWindowFromPointEx uFlags
        public const uint CWP_ALL = 0x0000;
        public const uint CWP_SKIPDISABLED = 0x0002;
        public const uint CWP_SKIPINVISIBLE = 0x0001;
        public const uint CWP_SKIPTRANSPARENT = 0x0004;
        //DeferWindowPos uFlags
        public static IntPtr HWND_BOTTOM = ((IntPtr)1);
        public static IntPtr HWND_NOTOPMOST = ((IntPtr) (- 2));
        public static IntPtr HWND_TOP = ((IntPtr)0);
        public static IntPtr HWND_TOPMOST = ((IntPtr) (- 1));
        //SetProcessDefaultLayout dwDefaultLayout
        public const uint LAYOUT_RTL = 0x00000001;
        //GetSysColor nIndex
        public const uint COLOR_3DDKSHADOW = 21;
        public const uint COLOR_3DFACE = 15;
        public const uint COLOR_3DHIGHLIGHT = 20;
        public const uint COLOR_3DHILIGHT = 20;
        public const uint COLOR_3DLIGHT = 22;
        public const uint COLOR_3DSHADOW = 16;
        public const uint COLOR_ACTIVEBORDER = 10;
        public const uint COLOR_ACTIVECAPTION = 2;
        public const uint COLOR_APPWORKSPACE = 12;
        public const uint COLOR_BACKGROUND = 1;
        public const uint COLOR_BTNFACE = 15;
        public const uint COLOR_BTNHIGHLIGHT = 20;
        public const uint COLOR_BTNHILIGHT = 20;
        public const uint COLOR_BTNSHADOW = 16;
        public const uint COLOR_BTNTEXT = 18;
        public const uint COLOR_CAPTIONTEXT = 9;
        public const uint COLOR_DESKTOP = 1;
        public const uint COLOR_GRADIENTACTIVECAPTION = 27;
        public const uint COLOR_GRADIENTINACTIVECAPTION = 28;
        public const uint COLOR_GRAYTEXT = 17;
        public const uint COLOR_HIGHLIGHT = 13;
        public const uint COLOR_HIGHLIGHTTEXT = 14;
        public const uint COLOR_HOTLIGHT = 26;
        //MouseInput dwFlags
        public const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        public const uint MOUSEEVENTF_HWHEEL = 0x01000;
        public const uint MOUSEEVENTF_MOVE = 0x0001;
        public const uint MOUSEEVENTF_MOVE_NOCOALESCE = 0x2000;
        public const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const uint MOUSEEVENTF_LEFTUP = 0x0004;
        public const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        public const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        public const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        public const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        public const uint MOUSEEVENTF_VIRTUALDESK = 0x4000;
        public const uint MOUSEEVENTF_WHEEL = 0x0800;
        public const uint MOUSEEVENTF_XDOWN = 0x0080;
        public const uint MOUSEEVENTF_XUP = 0x0100;
        //MouseInput mouseData
        public const uint XBUTTON1 = 0x0001;
        public const uint XBUTTON2 = 0x0002;
        //INPUT type
        public const uint INPUT_MOUSE = 0;
        public const uint INPUT_KEYBOARD = 1;
        public const uint INPUT_HARDWARE = 2;
        //Functions

        [DllImport(dllName,SetLastError =true)]
        public extern static IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport(dllName, SetLastError = true)]
        public extern static IntPtr GetParent(IntPtr hWnd);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool AdjustWindowRect(RECT lpRect, uint dwStyle,bool bMenu);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool AdjustWindowRectEx(RECT lpRect, uint dwStyle, bool bMenu,uint dwExStyle);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool AllowSetForegroundWindow(uint dwProcessId);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool AnimateWindow(IntPtr hwnd,uint dwTime,uint dwFlags);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool AnyPopup();
        [DllImport(dllName, SetLastError = true)]
        public extern static uint ArrangeIconicWindows(IntPtr hWnd);
        [DllImport(dllName, SetLastError = true)]
        public extern static object BeginDeferWindowPos(int nNumWindows);//RETURN N/A!
        [DllImport(dllName, SetLastError = true)]
        public extern static bool BringWindowToTop(IntPtr hWnd);
        [DllImport(dllName, SetLastError = true)]
        public extern static Int16 CascadeWindows(IntPtr hwndParent,uint wHow,RECT *lpRect,uint cKids,IntPtr* lpKids);//WARNING!
        [DllImport(dllName, SetLastError = true)]
        public extern static bool ChangeWindowMessageFilter(uint message,uint dwFlag);
        [DllImport(dllName, SetLastError = true)]
        public extern static IntPtr ChangeWindowMessageFilterEx(IntPtr hWnd,uint message, uint action);
        [DllImport(dllName, SetLastError = true)]
        public extern static IntPtr ChangeWindowMessageFilterEx(IntPtr hWndParent, POINT pt,uint uFlags);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool CloseWindow(IntPtr hWnd);
        [DllImport(dllName, SetLastError = true)]
        public extern static IntPtr CreateWindow(string lpClassName,string lpWindowName,uint dwStyle,int x,int y,int nWidth,int nHeight,IntPtr hWndParent,IntPtr hMenu,IntPtr hInstance,IntPtr lpParam);
        [DllImport(dllName, SetLastError = true)]
        public extern static IntPtr CreateWindowEx(uint dxExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);
        [DllImport(dllName, SetLastError = true)]
        public extern static IntPtr DeferWindowPos(IntPtr hWinPosInfo,IntPtr hWnd,IntPtr hWndInsertAfter,int x,int y,int cx,int cy,uint uFlags);//HWND_
        [DllImport(dllName, SetLastError = true)]
        public extern static bool DeregisterShellHookWindow(IntPtr hWnd);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool DestroyWindow(IntPtr hWnd);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool EndDeferWindowPos(IntPtr hWinPosInfo);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool EndTask(IntPtr hWnd,bool fShutDown,bool fForce);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool EnumChildWindows(IntPtr hWndParent, CallBack lpEnumFunc,IntPtr lParam);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool EnumThreadWindows(uint dwThreafId, CallBack lpfn, IntPtr lParam);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool EnumWindows(CallBack x, IntPtr y);
        [DllImport(dllName, SetLastError = true)]
        public extern static IntPtr FindWindow(string lpClassName,string lpWindowName);
        [DllImport(dllName, SetLastError = true)]
        public extern static IntPtr FindWindowEx(IntPtr hwndParent,IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool GetAltTabInfo(IntPtr hwnd, int iItem, tagALTTABINFO pati,out string pszItemText,uint cchItemText);
        [DllImport(dllName, SetLastError = true)]
        public extern static IntPtr GetAncestor(IntPtr hwnd, uint gaFlags);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool GetClientRect(IntPtr hwnd, out RECT lpRect);
        [DllImport(dllName, SetLastError = true)]
        public extern static IntPtr GetDesktopWindow();
        [DllImport(dllName, SetLastError = true)]
        public extern static IntPtr GetForegroundWindow();
        [DllImport(dllName, SetLastError = true)]
        public extern static bool GetGUIThreadInfo(uint idThread,out GUITHREADINFO LPGUITHREADINFO);
        [DllImport(dllName, SetLastError = true)]
        public extern static IntPtr GetLastActivePopup(IntPtr hWnd);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool GetLayeredWindowAttributes(IntPtr hWnd,out uint *pcrKey,byte *pbAlpha,uint* pdwFlags);
        [DllImport(dllName, SetLastError = true)]
        public extern static IntPtr GetMessageExtraInfo();
        [DllImport(dllName, SetLastError = true)]
        public extern static IntPtr GetNextWindow(IntPtr hWnd,uint wCmd);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool GetProcessDefaultLayout(out uint* pdwDefaultLayout);
        [DllImport(dllName, SetLastError = true)]
        public extern static IntPtr GetShellWindow();
        [DllImport(dllName, SetLastError = true)]
        public extern static uint GetSysColor(int nIndex);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool GetTitleBarInfo(IntPtr hwnd,out TITLEBARINFO pti);
        [DllImport(dllName, SetLastError = true)]
        public extern static IntPtr GetTopWindow(IntPtr hwnd);
        [DllImport(dllName, SetLastError = true)]
        public extern static IntPtr GetWindow(IntPtr hwnd,uint uCmd);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool GetWindowDisplayAffinity(IntPtr hwnd,out uint* dwAffinity);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool GetWindowInfo(IntPtr hwnd,out WINDOWINFO pwi);
        [DllImport(dllName, SetLastError = true)]
        public extern static uint GetWindowModuleFileName(IntPtr hwnd, out string lpszFileName,uint cchFileNameMax);
        [DllImport(dllName, SetLastError = true)]
        public extern static uint GetWindowPlacement(IntPtr hWnd,out WINDOWPLACEMENT* lpwndpl);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool GetWindowRect(IntPtr hwnd,out RECT *lpRect);
        [DllImport(dllName, SetLastError = true)]
        public extern static int GetWindowText(IntPtr hwnd, out string lpString, int nMaxCount);
        [DllImport(dllName, SetLastError = true)]
        public extern static int GetWindowTextLength(IntPtr hwnd);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool GetWindowThreadProcessId(IntPtr hwnd, out uint *lpdwProcessId);
        [DllImport(dllName, SetLastError = true)]
        public extern static int InternalGetWindowText(IntPtr hWnd, out string lpString, int nMaxCount);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool IsChild(IntPtr hWndParent, IntPtr hWnd);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool IsGUIThread(bool bConvert);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool IsHungAppWindow(IntPtr hWnd);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool IsIconic(IntPtr hWnd);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool IsProcessDPIAware();
        [DllImport(dllName, SetLastError = true)]
        public extern static bool IsWindow(IntPtr hWnd);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool IsWindowUnicode(bool hWnd);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool IsWindowVisible(IntPtr hWnd);
        [DllImport(dllName, SetLastError = true)]
        public extern static bool IsZoomed(IntPtr hWnd);
        [DllImport(dllName, SetLastError = true)]
        public extern static uint SendInput( uint uInputs,ref Input pInputs, int cbSize);
        
        //Struct
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

        }
        public struct POINT
        {
            public uint x;
            public uint y;
        }
        public struct tagALTTABINFO
        {
            public uint cbSize;
            public int cItems;
            public int cColumns;
            public int cRows;
           public  int iColFocus;
            public int iRowFocus;
            public int cxItem;
            public int cyItem;
            public POINT ptStart;
        }
        public struct GUITHREADINFO
        {
            public uint cbSize;
            public uint flags;
            public IntPtr IntPtrActive;
            public IntPtr IntPtrFocus;
            public IntPtr IntPtrCapture;
            public IntPtr IntPtrMenuOwner;
            public IntPtr IntPtrMoveSize;
            public IntPtr IntPtrCaret;
            public RECT rcCaret;
        }
        public struct TITLEBARINFO
        {
            public uint cbSize;
            public RECT rcTitleBar;
            public uint[]  rgstate;
        }
        public struct WINDOWINFO
        {
            public uint cbSize;
            public RECT rcWindow;
            public RECT rcClient;
            public uint dwStyle;
            public uint dwExStyle;
            public uint dwWindowStatus;
            public uint cxWindowBorders;
            public uint cyWindowBorders;
            public IntPtr WindowType;
            public Int16 wCreatorVersion;
        }
        public struct WINDOWPLACEMENT
        {
            public uint length;
            public uint flags;
            public uint showCmd;
            public POINT ptMinPosition;
            public POINT ptMaxPosition;
            public RECT rcNormalPosition;
        }

        /// <summary>
        /// i必為MOUSEINPUT/KEYBDINPUT/HARDWAREINPUT
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 28)]
        public struct Input
        {
            [FieldOffset(0)]
            public uint type;
            [FieldOffset(4)]
            public KEYBDINPUT ki;
            [FieldOffset(4)]
            public MOUSEINPUT mi;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;//
            public uint dwFlags;//MOUSEEVENTF
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct KEYBDINPUT
        {
            public Device.Keyboard.KeyCodes wVk;
            public Int16 wScan;
            public uint mouseData;//
            public uint dwFlags;//MOUSEEVENTF
            public uint time;
            public IntPtr dwExtraInfo;
        }
        //Delegate
        public delegate bool CallBack(IntPtr hwnd, IntPtr lParam);
        
        

    }
}
