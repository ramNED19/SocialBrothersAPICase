using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialBrothersAPICase.Model
{
    public class Filters
    {
        public string Straat { get; set; } = string.Empty;
        public int? Huisnummer { get; set; } = null;
        public string Toevoeging { get; set; } = string.Empty;
        public string Postcode { get; set; } = string.Empty;
        public string Plaats { get; set; } = string.Empty;
        public string Land { get; set; } = string.Empty;
    }
}
