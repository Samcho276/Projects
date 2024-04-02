using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Actions
{
    public static class _Converter
    {
        /*public static byte[,,] BmpToArr(Bitmap img)//destroys bitmap header
        {
            
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                var tmp = stream.ToArray();
                
                var tmpCounter = 0;
                var arr = new byte[img.Width,img.Height,3];
                
                for (int i = 0; i < img.Width; i++)
                    for (int j = 0; j < img.Height; j++)
                        for (int k = 0; k < 3; k++)
                            arr[i, j, k] = tmp[tmpCounter++];
                
                return arr;
            }
        }                
        public static Bitmap ArrToBmp(byte[,,] img) //destroys bitmap header
        {
            var arr = new byte[img.GetLength(0) * img.GetLength(1) * 3];
            var arrCounter = 0;
            for (int i = 0; i < img.GetLength(0); i++)
                for (int j = 0; j < img.GetLength(1); j++)
                    for (int k = 0; k < 3; k++)
                        arr[arrCounter++] = img[i, j, k];

            
            return (Bitmap)Image.FromStream(new MemoryStream(arr));
            
        }*/
        public static byte[] BitmapToByteArray(Bitmap bitmap)
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int byteCount = bmpData.Stride * bitmap.Height;
            byte[] byteArray = new byte[byteCount];

            Marshal.Copy(bmpData.Scan0, byteArray, 0, byteCount);

            bitmap.UnlockBits(bmpData);

            return byteArray;
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
