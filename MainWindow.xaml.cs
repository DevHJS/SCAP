using SCAP.Class;
using SCAP.windows;
using System.Windows;
using System.Windows.Input;

namespace SCAP;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    #region windows operations
    private void mainWindow_dragMove(object sender, MouseButtonEventArgs e) => DragMove();

    private void minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;


    private void close_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
    #endregion

    private void captureFullScreen_Click(object sender, RoutedEventArgs e)
    {
        this.Hide();
        var capture = new Capture();
        var img = capture.CaptureFullScreen();
        var viewer = new ViewerApp(img);
        viewer.Show();
    }
    private void capturePartScreen_Click(object sender, RoutedEventArgs e)
    {
        this.Hide();
        var cap = new capturingWindow();
        cap.Show();
    }
}
