using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Actions
{
    public class ImgTranslate
    {
        public Bitmap CopyRegionIntoImage(Bitmap srcBitmap,int xStart, int yStart,int xRange, int yRange, int xDest, int yDest) 
        {

            //zastapiane scale and duplicate
                /*var w = srcBitmap.Width;
                var h = srcBitmap.Height;

                var xShrink = 0;
                var yShrink = 0;

                if(xStart+xRange>w) 
                    xShrink = Math.Abs(w-xStart-xRange);
                if (yStart + yRange > h) 
                    yShrink = Math.Abs(h - yStart - yRange);

                var tmpBitmap = new Bitmap(xRange-xShrink, yRange-yShrink);

                for (int x = 1; x < xRange - xShrink; x++)
                    for (int y = 1; y < yRange - yShrink; y++) 
                        tmpBitmap.SetPixel(x,y, srcBitmap.GetPixel(xStart+x, yStart+y));

                var resBitmap = new Bitmap(w,h);

                int xExpand = w;
                int yExpand = h;

                if (xDest + xRange > w)
                    xExpand = xDest + xRange-xShrink;
                if (yDest + yRange > h) 
                    yExpand = yDest + yRange - yShrink;
                if(xExpand!=w || yExpand!=h )
                    resBitmap = ExpandMyBitmap(srcBitmap, xExpand, yExpand);


                for (int x = 1; x < tmpBitmap.Width; x++)
                   for (int y = 1; y < tmpBitmap.Height; y++)
                       resBitmap.SetPixel( xDest + x, yDest + y, tmpBitmap.GetPixel(x,y));

                return resBitmap;   */
                return srcBitmap;
        }
        private Bitmap ExpandMyBitmap(Bitmap from, int x, int y)
        {
            var tmp = new Bitmap(x,y);

            for (int i = 1;i<x; i++)
                for (int j = 1; j < y; j++)
                    tmp.SetPixel(i, j, Color.Purple);

            for (int i = 1; i < from.Width; i++)
                for (int j = 1; j < from.Height; j++)
                    tmp.SetPixel(i, j, from.GetPixel(i,j));

            return tmp;
        }
    }
}
