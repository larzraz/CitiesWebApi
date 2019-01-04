using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesWebApi.Model
{
    public class Attraction
    {

        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long CityID { get; set; }

    }
}
