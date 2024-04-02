using Klient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing;
using BackEnd;
using Newtonsoft.Json;
using System.Text;

namespace Klient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public IActionResult Index()
        {
            return View(new List<Bitmap>());
        }
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            Bitmap tmp;
            List<Bitmap> toView = new List<Bitmap>();

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                using (var img = Image.FromStream(memoryStream))
                {
                    tmp = new Bitmap(img);
                }
            }

            ImgObj toSend = _Converter.BitmapToByteArray(tmp);
            string apiUrl = "http://localhost:5220/api/Image";

            using (HttpClient httpClient = new HttpClient())
            {
                try{
                    string jsonBody = JsonConvert.SerializeObject(toSend);
                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode){
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ImgObj tmpImgObj = JsonConvert.DeserializeObject<ImgObj>(apiResponse);
                        toView.Add(_Converter.ByteArrayToBitmap(tmpImgObj.ImgArr, tmpImgObj.Width, tmpImgObj.Height));
                    }
                }
                catch (Exception ex){}
            }

            return View(toView);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}