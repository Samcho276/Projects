using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Actions
{
    public class ImgScale
    {
        public Bitmap ScaleAndDuplicate(Bitmap srcBitmap) //TODO: zrobić na bitach
        {
            var w = srcBitmap.Width;
            var h = srcBitmap.Height;

            //Rectangle srcRegion = new Rectangle(0, 0, w, h / 2);
            //Rectangle destRegion = new Rectangle(0, h/2, w*2, h);

            //System.Drawing.Imaging.PixelFormat format = srcBitmap.PixelFormat;
            //Bitmap cloneBitmap = srcBitmap.Clone(srcRegion, format);

            //using (Graphics grD = Graphics.FromImage(srcBitmap))
            //{
            //    grD.DrawImage(cloneBitmap, destRegion, srcRegion, GraphicsUnit.Pixel);
            //    return srcBitmap;
            //}           
            byte[] originalBytes = _Converter.BitmapToByteArray(srcBitmap);
            byte[] cloneBytes = _Converter.BitmapToByteArray(srcBitmap);
            DuplicateByteArray(originalBytes, cloneBytes, w, h);

            Bitmap resultBitmap = _Converter.ByteArrayToBitmap(originalBytes, w * 2, h);

            return resultBitmap;
        }
        static private void DuplicateByteArray(byte[] originalByteArray, byte[] cloneByteArray, int width, int height)
        {
            for (int y = 0; y < height/2; y++)
                for (int x = 0; x < width*2  ; x++)
                {
                    int originalIndex = (y * width + x) * 4;
                    int cloneIndex = (y * width + (x)) * 4;

                    for (int i = 0; i < 4; i++)
                        originalByteArray[originalIndex + i] = cloneByteArray[cloneIndex + i];
                }
        }
    }
}
