using System.Runtime.InteropServices;

namespace SCAP.Class;


[StructLayout(LayoutKind.Sequential)]
public struct RECT
{
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;

    public int Width { get => Right - Left; }
    public int Height { get => Bottom - Top; }
}
