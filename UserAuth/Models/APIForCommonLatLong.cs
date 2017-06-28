using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TapMap.Models
{
    public class APIForCommonLatLong
    {
        public List<APIForLongLat> APIForLongLatPlus { get; set; }
        public List<APIForLongLat> APIForLongLatMinus { get; set; }
    }
}