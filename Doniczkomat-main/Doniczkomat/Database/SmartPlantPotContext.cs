using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
namespace Doniczkomat.Database
{
    public class SmartPlantPotContext: DbContext
    {
        public DbSet<PlantPot> Pots { get; set; }
        //public DbSet<PumpOperations> PumpRecords { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=SmartPlantPots;Trusted_Connection=True;ConnectRetryCount=0");
        }

    }
}
