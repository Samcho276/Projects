using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.FileManager
{
    public class Loader
    {
        string directoryPath = ProjectPathConfig.InputDirectory;
        public List<ImData> ImageList()
        {
            string[] files = Directory.GetFiles(directoryPath);
            List<ImData> imageSet = new List<ImData>();

            foreach (string file in files)
            {
                LoadImage(file);
                imageSet.Add(new ImData(file, LoadImage(file)));
            }

            return imageSet;
        }
        private Image LoadImage(string path)
        {
            return Image.FromFile(path);
        }
        public Loader()
        {
        }
    }

}
