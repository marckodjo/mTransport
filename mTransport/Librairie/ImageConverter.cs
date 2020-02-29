using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace mTransport
{
    public class ImageConverter
    {
            public static Bitmap Resize(object obj, double width = 480, double height = 360)
            {
                var bitmap = obj.GetType() == typeof(Bitmap) ? (Bitmap)obj : null;
                var s = obj.GetType() == typeof(string) ? (string)obj : null;

                System.Windows.Size size = new System.Windows.Size(width, height);

                if (bitmap != null)
                    return Resize(bitmap, size);

                if (!string.IsNullOrEmpty(s))
                    return Resize(s, size);

                return null;
            }

            public static Bitmap Resize(string path, System.Windows.Size? size = null)
            {
                var m_size = size ?? new System.Windows.Size((double)480, (double)360);

                var bitmap = Bitmap.FromFile(path) as Bitmap;
                return Resize(bitmap, m_size);
            }

            public static Bitmap Resize(Bitmap p_bitmap, System.Windows.Size? size = null)
            {
                var m_size = size ?? new System.Windows.Size((double)480, (double)360);

                using (Bitmap m_bitmap = new Bitmap((int)m_size.Width, (int)m_size.Height))
                {
                    using (Graphics graphics = Graphics.FromImage(m_bitmap))
                    {
                        graphics.Clear(Color.FromArgb(-1));

                        if ((float)p_bitmap.Width / (float)m_size.Width == (float)p_bitmap.Height / (float)m_size.Height) //Target size has a 1:1 aspect ratio
                            graphics.DrawImage(p_bitmap, 0, 0, (int)m_size.Width, (int)m_size.Height);

                        else if ((float)p_bitmap.Width / (float)m_size.Width > (float)p_bitmap.Height / (float)m_size.Height) //There will be white space on the top and bottom
                            graphics.DrawImage(p_bitmap, 0f, (float)m_bitmap.Height / 2f - (p_bitmap.Height * ((float)m_bitmap.Width / (float)p_bitmap.Width)) / 2f, (float)m_bitmap.Width, p_bitmap.Height * ((float)m_bitmap.Width / (float)p_bitmap.Width));

                        else if ((float)p_bitmap.Width / (float)m_size.Width < (float)p_bitmap.Height / (float)m_size.Height) //There will be white space on the sides
                            graphics.DrawImage(p_bitmap, (float)m_bitmap.Width / 2f - (p_bitmap.Width * ((float)m_bitmap.Height / (float)p_bitmap.Height)) / 2f, 0f, p_bitmap.Width * ((float)m_bitmap.Height / (float)p_bitmap.Height), (float)m_bitmap.Height);

                        graphics.Save();

                        using (MemoryStream stream = new MemoryStream())
                        {
                            m_bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                            var bitmap = new Bitmap(stream);
                            stream.Close();

                            return bitmap;
                        }
                    }
                }
            }

            public static Bitmap FromArray(byte[] array)
            {
                if (array == null)
                    return null;

                Bitmap image;

                using (var stream = new MemoryStream(array))
                {
                    image = new Bitmap(stream);
                }

                return image;
            }

            public static byte[] ToArray(Bitmap bitmap)
            {
                if (bitmap == null)
                    return null;

                byte[] array = new byte[0];
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    stream.Close();

                    array = stream.ToArray();
                }

                return array;
            }

            public static Bitmap FromImage(BitmapSource image)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    BitmapEncoder encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(image));
                    encoder.Save(stream);

                    var bitmap = new Bitmap(stream);
                    return new Bitmap(bitmap);
                }
            }

            [System.Runtime.InteropServices.DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr obj);

            public static BitmapSource ToImage(Bitmap bitmap)
            {
                IntPtr b = bitmap.GetHbitmap();
                BitmapSource source;
                try
                {
                    source = Imaging.CreateBitmapSourceFromHBitmap(b, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

                }
                finally
                {
                    DeleteObject(b);
                }

                return source;
            }

            public static string BitmapToHexString(Bitmap bitmap)
            {
                var array = ToArray(bitmap);
                return Hex.ToHexString(array);
            }

            public static Bitmap BitmapFromHexString(string data)
            {
                var array = Hex.FromHexString(data);
                return FromArray(array);
            }

            public static string ToHexString(BitmapSource image)
            {
                var bitmap = FromImage(image);
                return BitmapToHexString(bitmap);
            }

            public static BitmapSource FromHexString(string data)
            {
                var bitmap = BitmapFromHexString(data);
                return ToImage(bitmap);
            }
        }
    }
