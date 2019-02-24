using System;
using System.Windows;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Drawing;
using System.Drawing.Imaging;

namespace StatusBuilder
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Drawing.Bitmap bmpScreenshot = new System.Drawing.Bitmap((int)CanvasPrint.ActualWidth, (int)CanvasPrint.ActualHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            System.Drawing.Point p = System.Drawing.Point.Empty;
            gfxScreenshot.CopyFromScreen(new System.Drawing.Point((int)Left, (int)Top), p, new System.Drawing.Size(width: 800, height: 800));//Screen size

            PdfDocument doc = new PdfDocument();
            doc.Pages.Add(new PdfPage());
            Stream memoryStream = new MemoryStream();
            bmpScreenshot.Save(memoryStream, ImageFormat.Bmp);
            XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[0]);
            XImage img = XImage.FromStream(memoryStream);
            xgr.DrawImage(img, -6, -24);

            string user_Url_Desctop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            doc.Save($"{user_Url_Desctop}\\STATUS {textBoxNameFile.Text}.pdf");//Url to save Name pdf file
            doc.Close();
            MessageBox.Show($"File STATUS {textBoxNameFile.Text}.pdf Save to your Desctop");
            System.Diagnostics.Process.Start($"{user_Url_Desctop}\\STATUS {textBoxNameFile.Text}.pdf");//Url open file pdf

        }
    }
}
