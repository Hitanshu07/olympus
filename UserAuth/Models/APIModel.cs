using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TapMap.Models
{
    public class APIModel
    {
        public int WashroomId { get; set; }
        public string WashroomName { get; set; }
        public string LocationDescription { get; set; }
        public string Address { get; set; }
        public int Rating { get; set; }
        public int? RatingCount { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string UserToiletGPSLocation { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

  

        public string ImageUrl { get; set; }

     

        public bool? Feature_ToiletRoll { get; set; }
        public bool? Feature_DiaperChangingRooms { get; set; }
  

        public bool? Feature_SpecialWashrooms { get; set; }
        public bool? Feature_Male { get; set; }
        public bool? Feature_Female { get; set; }
        public bool? Feature_Unisex { get; set; }

        public int Rating1 { get; set; }
        public int Rating2 { get; set; }
        public int Rating3 { get; set; }
        public int Rating4 { get; set; }
        public int Rating5 { get; set; }



        public List<WashroomImagesModel> ToiletImages { get; set; }
   


    }
}