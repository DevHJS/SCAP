using System;
using System.Runtime.InteropServices;

namespace SCAP.Class;


static class Win32Native
{
    #region capturing methods 

    // for full screen
    public const int DESKTOPVERTRES = 0x75;
    public const int DESKTOPHORZRES = 0x76;

    [DllImport("gdi32.dll")]
    public static extern int GetDeviceCaps(IntPtr hDC, int index);

    //for region capture
    public const uint MONITOR_DEFAULTTONEAREST = 0x00000002;

    public enum MonitorDPIType
    {
        EFFECTIVE_DPI = 0,
        ANGULAR_DPI = 1,
        RAW_DPI = 2
    }
    [DllImport("shcore.dll")]
    public static extern int GetDpiForMonitor(IntPtr hMonitor, MonitorDPIType dpiType, out uint dpiX, out uint dpiY);
    [DllImport("user32.dll")]
    public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

    #endregion

}