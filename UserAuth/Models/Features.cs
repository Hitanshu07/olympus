using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TapMap.Models
{
    public class Features
    {
        public bool? ToiletRoll { get; set; }
        public bool? DiaperChangingRooms { get; set; }
        public bool? SpecialWashrooms { get; set; }
        public bool? Male { get; set; }
        public bool? Female { get; set; }
        public bool? Unisex { get; set; }
  
        public int WashroomId { get; set; }

    }
}