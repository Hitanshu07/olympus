using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UserAuth.Helpers
{
  public static class ConfigHelper
  {
    public static T GetAppSetting<T>(string configKey, Func<string, T> valueParser)
    {
      var value = ConfigurationManager.AppSettings[configKey];
      return valueParser(value);
    }
  }
}