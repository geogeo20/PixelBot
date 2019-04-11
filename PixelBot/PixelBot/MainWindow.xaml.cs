using System;
using System.Windows;
using System.Drawing;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace PixelBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonSearchPixel_Click(object sender, RoutedEventArgs e)
        {
            string inputHexColorCode = TextBoxHexColor.Text;



            SearchPixel(inputHexColorCode);



        }


        private bool SearchPixel(string hexcode)
        {
            

            // Bitmap bitmap = new Bitmap(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);

            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            Graphics graphics = Graphics.FromImage(bitmap as Image);

            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);

            Color desiredColor = ColorTranslator.FromHtml(hexcode);

            for(int x=0; x < screenHeight; x++)
            {
                for(int y=0; x < screenWidth; y++)
                {
                    Color curentPixelColor = bitmap.GetPixel(x, y);
                    if(desiredColor == curentPixelColor)
                    {
                        MessageBox.Show(String.Format("Found Pixel at {0}, {1} - Now set mouse cursoor", x,y));
                        return true;
                    }
                }
            }


            return false;
        }

    }
}
