namespace Doniczkomat.Database
{
    public class PlantPot
    {
        public int id { get; set; }
        public string plantPotID { get; set; }
        public string name { get; set; }
        public string ip { get; set; }
        public int waterLevel { get; set; }
        public int soilMoisture { get; set; }
        public DateTime lastWatering { get; set; }
        public int cooldown { get; set; }

        public DateTime lastCheck { get; set; }

        public PlantPot(PlantPot x)
        {
            this.plantPotID = x.plantPotID;
            this.name = x.name;
            this.ip = x.ip;
            this.waterLevel = x.waterLevel;
            this.soilMoisture = x.soilMoisture;
            this.lastWatering = x.lastWatering;
            this.cooldown = x.cooldown;
            this.lastCheck = x.lastCheck;
        }

        public PlantPot()
        {
        }
    }
}
