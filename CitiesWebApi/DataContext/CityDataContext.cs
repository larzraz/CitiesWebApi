using CitiesWebApi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesWebApi.DataContext
{
    public class CityDataContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Attraction> Attractions { get; set; }
        public CityDataContext(DbContextOptions<CityDataContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            if (Cities.Count() == 0)
            {
                Cities.Add(new City { Name = "Odense", Description = "Smukkeste By", Attractions = new List<Attraction> { new Attraction { Name = "Døckerslundbageren", Description = "Best Bager i verden" }, new Attraction { Name = "Larz' crib", Description = "Det fedeste hus i verden" } } });
                Cities.Add(new City { Name = "Kerteminde", Description = "En anden by i Danmark", Attractions = new List<Attraction> { new Attraction { Name = "Amanda", Description = "En statue af en kælling" }, new Attraction { Name = "Vaffelhuset", Description = "Verdens Bedste Ishus" } } });
               
                SaveChanges();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
          
        }
    }
}
