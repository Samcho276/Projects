using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Actions
{
    public class ImgFlip
    {
        public Bitmap FlipVertical(Bitmap srcBitmap)
        {
            var width = srcBitmap.Width;
            var height = srcBitmap.Height;
            var byteArray = _Converter.BitmapToByteArray(srcBitmap);

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width / 2; x++)
                {
                    int index1 = (y * width + x) * 4; //pixel 4byte
                    int index2 = (y * width + (width - 1 - x)) * 4;

                    for (int i = 0; i < 4; i++)
                    {
                        byte temp = byteArray[index1 + i];
                        byteArray[index1 + i] = byteArray[index2 + i];
                        byteArray[index2 + i] = temp;
                    }
                }

            return _Converter.ByteArrayToBitmap(byteArray, width, height);
        }
        public Bitmap FlipHorizontal(Bitmap srcBitmap)
        {
            var width = srcBitmap.Width;
            var height = srcBitmap.Height;

            var byteArray = _Converter.BitmapToByteArray(srcBitmap);

            for (int y = 0; y < height / 2; y++)            
                for (int x = 0; x < width * 4; x++)
                {
                    int index1 = y * width * 4 + x;
                    int index2 = (height - 1 - y) * width * 4 + x;

                    byte temp = byteArray[index1];
                    byteArray[index1] = byteArray[index2];
                    byteArray[index2] = temp;
                }

            return _Converter.ByteArrayToBitmap(byteArray, width, height);
        }
    }
}
