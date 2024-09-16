using Doniczkomat.Database;
using Doniczkomat.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata;
using Newtonsoft.Json;
using Microsoft.Identity.Client;
namespace Doniczkomat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient client = new HttpClient();
        private readonly int SoilMoistureThreashold = 30;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var tmp =new List<PlantPot>();
            using (var db = new SmartPlantPotContext())
            {
                foreach (var item in db.Pots.OrderByDescending(x => x.lastCheck).Take(db.Pots.Select(x => x.plantPotID).Distinct().Count()))
                    tmp.Add(item);
            }
            if(tmp.Count > 0){
                await CheckPlants(tmp);
            }
            
            return View(nameof(MyPlants), tmp);
        }
        public IActionResult MyPlants()
        {
            var tmp = new List<PlantPot>();
            using (var db = new SmartPlantPotContext())
            {
                foreach (var item in db.Pots.OrderByDescending(x => x.lastCheck).Take(db.Pots.Select(x => x.plantPotID).Distinct().Count()))
                    tmp.Add(item);
            }                     
            return View(tmp);
        }
        public IActionResult PlantPotDetails(string potID) 
        {
            var tmp = new List<PlantPot>();
            using (var db = new SmartPlantPotContext())
            {
                foreach (var item in db.Pots.Where(x => x.plantPotID == potID).OrderByDescending(x => x.lastCheck).ToList())
                    tmp.Add(item);
            }
            return View(tmp.Take(365).ToList());
        }
        public IActionResult UpdatePlantPotDetails(string potID,string potName,int potCooldown)
        {
            var tmp = new List<PlantPot>();
            using (var db = new SmartPlantPotContext())
            {
                foreach (var item in db.Pots.Where(x => x.plantPotID == potID).ToList())
                    tmp.Add(item);
                foreach (var item in tmp)
                {
                    item.name = potName;
                    item.cooldown = potCooldown;
                    db.Pots.Update(item);
                }
                db.SaveChanges();
            }
            return RedirectToAction(nameof(MyPlants));
        }

        public IActionResult AddSmartPlantPot()
        {

            return View();
        }
        public async Task<IActionResult> AddPlantPotToDatabase(SmartPlantPotModel model)
        {
            if (ModelState.IsValid)
            {
                /*var requestAdress = "http://" + model.ip;
                HttpResponseMessage response = await client.GetAsync(requestAdress + "/STATUS");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var fromPot = JsonConvert.DeserializeObject<PotData>(responseBody);
                */
                var fromPot = await UpdatePlantsStatus(model.ip);

                if (IsPotAlreadyRegistered(fromPot.name))
                    return View(nameof(Index));

                var NewPot = new PlantPot
                {
                    plantPotID = fromPot.name,
                    name = model.name,
                    ip = model.ip,
                    waterLevel = fromPot.waterLevel,
                    soilMoisture = fromPot.soilMoisture,
                    lastWatering = DateTime.Now,
                    cooldown = CheckCooldown(model.cooldown),
                    lastCheck = DateTime.Now,
                };

                using (var db = new SmartPlantPotContext())
                {
                    db.Pots.Add(NewPot);
                    db.SaveChanges();
                }
                return View(nameof(Index));
            }
            return View(nameof(AddSmartPlantPot));
        }
        private int CheckCooldown(int a)
        {
            if (a < 0) return a * (-1);
            return a;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private bool IsPotAlreadyRegistered(string potID)
        {
            int tmp;
            using (var db = new SmartPlantPotContext())
            {
                tmp = db.Pots.Select(x => x.plantPotID == potID).Count();
            }
            if (tmp > 0) return true;
            return false;
        }
        
        
        private async Task<bool> CheckPlants(List<PlantPot> myPlants)
        {
            PotData tmp;
            PlantPot tmpPot;
            foreach (var item in myPlants)
            {
                
                tmp = await UpdatePlantsStatus(item.ip);
                item.lastCheck=DateTime.Now;
                item.waterLevel = tmp.waterLevel;
                item.soilMoisture = tmp.soilMoisture;
                
                    
                if (item.soilMoisture < SoilMoistureThreashold)
                {
                    if (DateTime.Now.AddDays((-1) * item.cooldown) > item.lastWatering)
                    {
                        item.lastWatering = DateTime.Now;
                        ActivateWatering(item.ip, tmp.name);
                    }
                }
                tmpPot = new PlantPot(item);
                using (var db = new SmartPlantPotContext())
                {
                    db.Pots.Add(tmpPot);
                    db.SaveChanges();
                }
            }
            return true;
        }
        private async Task<PotData> UpdatePlantsStatus(string ip)
        {
            try
            {
                var requestAdress = "http://" + ip;
                HttpResponseMessage response = await client.GetAsync(requestAdress + "/STATUS");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var tmp = JsonConvert.DeserializeObject<PotData>(responseBody);
                return tmp;
            } catch { };
            return new PotData { name = ip + "did not respond", soilMoisture = 0, waterLevel = 0 };


        }
        private async void ActivateWatering(string ipAdress, string name)
        {
            try
            {


                HttpContent emptyContent = new StringContent(string.Empty);
                HttpResponseMessage response = await client.PostAsync("http://" + ipAdress + "/PUMP", emptyContent);
            } catch { }
        }
    }
    class PotData
    {
        public string name { get; set; }
        public int waterLevel  { get; set; }
        public int soilMoisture { get; set; }
    }
}
