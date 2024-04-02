using BackEnd;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using Newtonsoft.Json;
namespace ZTP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] ImgObj byteImage)
        {
            
            var editor = new BackEnd.Repository();            
            var res = byteImage;
            res = editor.ImgFlip.FlipHorizontal(res);
            res = editor.ImgFlip.FlipVertical(res);

            return Ok(res);
        }

    }
}
