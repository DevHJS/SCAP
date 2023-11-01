using SCAP.Class;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SCAP.windows;

public partial class capturingWindow : Window
{
    private Point Start;

    private Point Current;

    private bool isDrawing = false;

    private double X, Y, W, H;

    public capturingWindow()
    {
        InitializeComponent();
    }

    private void CapGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        isDrawing = true;

        Start = Mouse.GetPosition(Canvas1);

        Canvas.SetLeft(Rect, Start.X);
        Canvas.SetTop(Rect, Start.Y);

    }

    private void CapGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        isDrawing = false;
        var cap = new Capture();
        this.Hide();
        BitmapSource bSource = cap.captureScreenRegion((int)X, (int)Y, (int)W, (int)H, this);
        var viewer = new ViewerApp(bSource);
        viewer.Show();

    }

    private void CapGrid_MouseMove(object sender, MouseEventArgs e)
    {
        if (isDrawing)
        {
            // Get new position
            Current = Mouse.GetPosition(Canvas1);

            // Calculate rectangle cords/size
            X = Math.Min(Current.X, Start.X);
            Y = Math.Min(Current.Y, Start.Y);
            W = Math.Max(Current.X, Start.X) - X;
            H = Math.Max(Current.Y, Start.Y) - Y;

            Canvas.SetLeft(Rect, X);
            Canvas.SetTop(Rect, Y);

            // Update rectangle
            Rect.Width = W;
            Rect.Height = H;
            Rect.SetValue(Canvas.LeftProperty, X);
            Rect.SetValue(Canvas.TopProperty, Y);

            // Toogle visibility
            if (Rect.Visibility != Visibility.Visible)
                Rect.Visibility = Visibility.Visible;
        }
    }
}

