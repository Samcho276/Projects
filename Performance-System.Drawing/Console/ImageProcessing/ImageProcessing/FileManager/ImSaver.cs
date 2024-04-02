using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ImageProcessing.FileManager
{
    public class ImSaver
    {
        string outputPath = ProjectPathConfig.OutputDirectory;

        public void SaveAll(List<ImData> imgSet)
        {
            foreach (ImData img in imgSet)
                ImSave(img);
        }
        public void ImSave(ImData img)
        {
            img.MyImg.Save(outputPath + Path.GetFileName(img.Name));
        }
        public ImSaver()
        {
        }
    }
}
