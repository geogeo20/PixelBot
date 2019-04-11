using System;
using System.Windows;
using System.Drawing;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using System.Runtime.InteropServices;

namespace PixelBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const UInt32 mouseUp = 0x0002;
        private const UInt32 mouseDown = 0x0004;

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInf);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Click()
        {
            mouse_event(mouseDown, 0, 0, 0, 0);
            mouse_event(mouseUp, 0, 0, 0, 0);

        }
        private void DoubleClick(int posX, int posY)
        {
            SetCursorPos(posX, posX);
            Click();
            System.Threading.Thread.Sleep(250);
            Click();
        }


        private void ButtonSearchPixel_Click(object sender, RoutedEventArgs e)
        {
            string inputHexColorCode = TextBoxHexColor.Text;



            SearchPixel(inputHexColorCode);

             

        }


        private bool SearchPixel(string hexcode)
        {
            

            Bitmap bitmap = new Bitmap(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);

            //Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            int screenWidth = SystemInformation.VirtualScreen.Width;
            int screenHeight = SystemInformation.VirtualScreen.Height;

            Graphics graphics = Graphics.FromImage(bitmap as Image);

            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);

            Color desiredColor = ColorTranslator.FromHtml(hexcode);

            for(int x=0; x < screenWidth; x++)
            {
                for(int y=0; y < screenHeight; y++)
                {
                    Color curentPixelColor = bitmap.GetPixel(x, y);
                    if(desiredColor == curentPixelColor)
                    {
                        MessageBox.Show(String.Format("Found Pixel at {0}, {1} - Now set mouse cursoor", x,y));
                        DoubleClick(x, y);
                        return true;
                    }
                }
            }




            return false;
        }

    }
}
