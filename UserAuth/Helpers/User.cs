using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;

namespace TapMap.Helper
{
    public class User
    {
        public static string UserPhotoPath = ConfigurationManager.AppSettings["UserPhoto"].ToString(CultureInfo.InvariantCulture);
      
    }
}