using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.FileManager
{
    public class ImData
    {
        public string Name { get; set; }
        public Image MyImg { get; set; }

        public ImData(string name, Image image)
        {
            Name = name;
            MyImg = image;
        }
    }
}
