using ImageProcessing.FileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Actions
{
    public class ActionsRepository
    {
        public ImgTranslate ImgTranslateToCenter = new();
        public ImgFlip ImgFlip = new();
        public ImgScale ImgScale = new();
        public Loader ImgLoader = new();
        public ImSaver ImgSaver = new();
    }
}
