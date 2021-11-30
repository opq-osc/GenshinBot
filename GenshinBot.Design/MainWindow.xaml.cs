using GenshinBot.Design.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GenshinBot.Design
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var page = new OverView();
            frame.Content = page;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(1080, 2340, 192, 192, 
                PixelFormats.Pbgra32);
            renderTargetBitmap.Render(frame.Content as Visual);
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Clear();
            encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            var fs = File.Open("overview.png", FileMode.Create);
            encoder.Save(fs);
            fs.Close();
        }
    }
}
