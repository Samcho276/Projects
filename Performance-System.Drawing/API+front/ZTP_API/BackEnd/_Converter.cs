using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class _Converter
    {
        public static ImgObj BitmapToByteArray(Bitmap bitmap)
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int byteCount = bmpData.Stride * bitmap.Height;
            byte[] byteArray = new byte[byteCount];

            Marshal.Copy(bmpData.Scan0, byteArray, 0, byteCount);

            bitmap.UnlockBits(bmpData);

            return new ImgObj(bitmap.Height,bitmap.Width,byteArray);
        }

        public static Bitmap ByteArrayToBitmap(byte[] byteArray, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);

            Marshal.Copy(byteArray, 0, bmpData.Scan0, byteArray.Length);

            bitmap.UnlockBits(bmpData);

            return bitmap;
        }
    }
}
