using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
namespace TapMap.Helper
{
    public class Washroom
    {

        public static string WashroomImagePath = ConfigurationManager.AppSettings["WashroomImagePath"].ToString(CultureInfo.InvariantCulture);
        public static string BasePath = ConfigurationManager.AppSettings["BasePath"].ToString(CultureInfo.InvariantCulture);

    }
}