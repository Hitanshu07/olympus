using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserAuth.Helpers
{
  public static class AppConstants
  {
    public const string SUCCESS = "Success";
    public const string FAILURE = "Failure";
    public const string PENDING = "Pending";
    public const string TEST_GUID = "051386ec016c46df94d23ff8ef23f276";
    public const string DEFAULT_CLIENT_ID = "cbce9aac0754483b9d0b3441e322887c";
    public const string TEST_ENCRYPT_KEY = "86ab8d49162a4dd5a4f173c841bb60b6";
    public const string ProviderNameSystem = "System";
    public const string ProviderNameFacebook = "Facebook";
    public const string ProviderNameGoogle = "Google";
    public const string ProviderNameVerify = "Verify";
    public const string Version = "1.0";
    public const string GuidFormatter = "n";
    public const int DEFAULT_TOKEN_VALID_DURATION_MINUTES = 600;
    public static DateTime MinDateForsql = new DateTime(1800, 1, 1);
    public static string EmailSmtpServer = ConfigHelper.GetAppSetting("EmailSmtpServer", val => val ?? string.Empty);
    public static string EmailFromEmailId = ConfigHelper.GetAppSetting("EmailFromEmailId", val => val ?? string.Empty);
    public static string EmailFromPassword = ConfigHelper.GetAppSetting("EmailFromPassword", val => val ?? string.Empty);
    public static int EmailFromPort = ConfigHelper.GetAppSetting("EmailFromPort", val => int.Parse(string.IsNullOrEmpty(val) ? "0" : val));
    public static string UrlBasePath = ConfigHelper.GetAppSetting("UrlBasePath", val => val ?? string.Empty);
    public static int VerficationEmailExpiryHours = 60;
    public const string ActionVerify = "verify";
    public const string ActionChangePasword = "change-password";
    public const string ActionForgotPasword = "forgot-password";
  }
}