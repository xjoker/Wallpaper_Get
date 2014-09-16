using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wallpaper_Get
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int SW = Screen.PrimaryScreen.Bounds.Width;  
            Uri uri=null;
            if(SW.ToString()=="1440")
            {
                uri = new Uri("http://10.10.168.80/2000-1440.jpg");
            }
            else if  (SW.ToString() == "1024")
            {
                uri = new Uri("http://10.10.168.80/2000-1024.jpg");
            }
            else if (SW.ToString() == "1366")
            {
                uri = new Uri("http://10.10.168.80/2000-1366.jpg");
            }
            else
            {
                uri = new Uri("http://10.10.168.80/2000.jpg");
            }
            getWallpaper.Wallpaper.Set(uri, getWallpaper.Style.Centered);
            Application.Exit();
        }
    }
}
