using Microsoft.Win32;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SCAP.windows;

public partial class ViewerApp : Window
{
    private BitmapSource _source;
    public ViewerApp(BitmapSource source)
    {
        InitializeComponent();
        ViewImg.Source = source;
        _source = source;
    }
    #region windows operations
    private void Window_dragMove(object sender, MouseButtonEventArgs e) => DragMove();

    private void close_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
    #endregion

    private void SaveImage(object sender, RoutedEventArgs e)
    {
        try
        {
            using MemoryStream stream = new MemoryStream();

            BitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(_source));
            encoder.Save(stream);
            var img = new Bitmap(stream);

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPEG Image|*.jpg";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                img.Save(filePath);
            }
        }
        catch
        {
            MessageBox.Show("image couldn't be saved");
        }
        finally
        {
            Application.Current.Shutdown();
        }


    }

    private void RetakeImage(object sender, RoutedEventArgs e)
    {
        this.Hide();
        var main = new MainWindow();
        main.Show();

    }

}
