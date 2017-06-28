using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TapMap.Models
{
    public class UpdateRatingDetailsAPI
    {
        public int? RatingCount { get; set; }
        public int CurrentRating{get; set;}
        public int Rating1 { get; set; }
        public int Rating2 { get; set; }
        public int Rating3 { get; set; }
        public int Rating4 { get; set; }
        public int Rating5 { get; set; }
    }
}