using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class ImgObj
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public byte[] ImgArr { get; set; }

        public ImgObj(int height, int width, byte[] imgArr)
        {
            Height = height;
            Width = width;
            ImgArr = imgArr;
        }
    }
}
