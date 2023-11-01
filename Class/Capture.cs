using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;


namespace SCAP.Class;

class Capture
{
    public BitmapSource CaptureFullScreen()
    {
        int screenWidth, screenHeight;

        using var bounds = Graphics.FromHwnd(IntPtr.Zero);
        var hDC = bounds.GetHdc();
        screenWidth = Win32Native.GetDeviceCaps(hDC, Win32Native.DESKTOPHORZRES);
        screenHeight = Win32Native.GetDeviceCaps(hDC, Win32Native.DESKTOPVERTRES);
        bounds.ReleaseHdc(hDC);

        var screenshot = new Bitmap(screenWidth, screenHeight);

        using Graphics graphics = Graphics.FromImage(screenshot);
        graphics.CopyFromScreen(0, 0, 0, 0, screenshot.Size);

        BitmapSource source = Imaging.CreateBitmapSourceFromHBitmap(
        screenshot.GetHbitmap(),
        IntPtr.Zero,
        Int32Rect.Empty,
        BitmapSizeOptions.FromEmptyOptions());

        return source;

    }


    public BitmapSource captureScreenRegion(int Left, int Top, int Width, int Height, Window win)
    {

        IntPtr hWnd = new WindowInteropHelper(win).Handle;
        IntPtr hMonitor = Win32Native.MonitorFromWindow(hWnd, Win32Native.MONITOR_DEFAULTTONEAREST);

        uint dpiX, dpiY;
        Win32Native.GetDpiForMonitor(hMonitor, Win32Native.MonitorDPIType.EFFECTIVE_DPI, out dpiX, out dpiY);
        Left = (int)(Left * dpiX / 96.0);
        Top = (int)(Top * dpiY / 96.0);
        Width = (int)(Width * dpiX / 96.0);
        Height = (int)(Height * dpiY / 96.0);

        Capture screenCapture = new Capture();
        BitmapSource fullScreenCapture = screenCapture.CaptureFullScreen();
        Int32Rect regionRect = new Int32Rect(Left, Top, Width, Height);
        CroppedBitmap croppedBitmap = new CroppedBitmap(fullScreenCapture, regionRect);
        return croppedBitmap;
    }



}


