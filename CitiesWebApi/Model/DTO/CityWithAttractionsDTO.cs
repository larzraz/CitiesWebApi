using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesWebApi.Model.DTO
{
    public class CityWithAttractionsDTO
    {

        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Attraction> Attractions { get; set; }

    }
}
