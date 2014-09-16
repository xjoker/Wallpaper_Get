using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace Wallpaper_Get
{
    class getWallpaper
    {
        public enum Style : int
        {
            Tiled,
            Centered,
            Stretched
        }
        public sealed class Wallpaper
        {
            Wallpaper() { }
            const int SPI_SETDESKWALLPAPER = 20;
            const int SPIF_UPDATEINIFILE = 0x01;
            const int SPIF_SENDWININICHANGE = 0x02;
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            //uiAction是作不同的操作参数。
            //uiParam是设置的参数。
            //pvParam是设置或返回的参数。
            //fWinIni是设置的参数。
            static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

            //壁纸的几种模式的枚举
            
            public static void Set(Uri uri, Style style)
            {
                try
                {
                    System.IO.Stream s = new WebClient().OpenRead(uri.ToString());
                    //获取壁纸文件，传入到流
                    System.Drawing.Image img = System.Drawing.Image.FromStream(s);
                    //传入到image中
                    string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
                    //将壁纸的存储地址设置为Temp目录，并且命名为wallpaper.bmp
                    img.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);
                    //保存为bmp文件
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
                    //打开注册表
                    //判断壁纸设置的类型，并且将键值写入注册表
                    if (style == Style.Stretched)
                    {
                        key.SetValue(@"WallpaperStyle", 2.ToString());
                        key.SetValue(@"TileWallpaper", 0.ToString());
                    }
                    if (style == Style.Centered)
                    {
                        key.SetValue(@"WallpaperStyle", 1.ToString());
                        key.SetValue(@"TileWallpaper", 0.ToString());
                    }
                    if (style == Style.Tiled)
                    {
                        key.SetValue(@"WallpaperStyle", 1.ToString());
                        key.SetValue(@"TileWallpaper", 1.ToString());
                    }
                    //调用方法设置壁纸
                    SystemParametersInfo(SPI_SETDESKWALLPAPER,
                        0,
                        tempPath,
                        SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
                }
                catch
                {

                }
                
            }
        }

    }
}