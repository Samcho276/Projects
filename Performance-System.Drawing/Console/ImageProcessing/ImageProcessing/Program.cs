using System.Collections.Generic;
using System.Drawing;
using ImageProcessing.Actions;
using ImageProcessing.FileManager;

internal class Program
{
    public static void Main(string[] args)
    {
        var newImageSet = new List<ImData>();
        var editor = new ActionsRepository();
        var imageSet = editor.ImgLoader.ImageList();
        
        foreach (var item in imageSet)
        {

            var tmp = new Bitmap(item.MyImg);
            var res = new Bitmap(tmp);            
            res = editor.ImgFlip.FlipHorizontal(res);
            res = editor.ImgFlip.FlipVertical(res);            
            //res = editor.ImgScale.ScaleAndDuplicate(res);
            //res = editor.ImgTranslateToCenter.CopyRegionIntoImage(res,220,220,1000,1000,300,300);

            newImageSet.Add
            (
                new ImData
                (
                    item.Name,
                    res
                )
            );
        }
        editor.ImgSaver.SaveAll(newImageSet);
    }

}