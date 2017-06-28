using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UserAuth.ApiModels;

namespace UserAuth.Helpers
{
  public class Utility
  {
    public static bool SendEmail(string toName, string toEmailId, string subject, string body)
    {
      bool result;
      var fromAddress = new MailAddress(AppConstants.EmailFromEmailId);
      var toAddress = new MailAddress(toEmailId, toName);
      var fromPassword = AppConstants.EmailFromPassword;
      try
      {
        var smtp = new SmtpClient
        {
          Host = AppConstants.EmailSmtpServer,
          Port = AppConstants.EmailFromPort,
          EnableSsl = true,
          DeliveryMethod = SmtpDeliveryMethod.Network,
          UseDefaultCredentials = false,
          Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
          Timeout = 20000
        };
        using (var message = new MailMessage(fromAddress, toAddress)
        {
          Subject = subject,
          Body = body
        })
        {
          message.IsBodyHtml = true;
          smtp.Send(message);
          result = true;
        }
      }
      catch (Exception)
      {
        result = false;
      }

      return result;
    }

    public static string UrlTokenEncode(string str)
    {
      return HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(str));
    }

    public static string UrlTokenDecode(string str)
    {
      var urlTokenDecoded = HttpServerUtility.UrlTokenDecode(str);
      return urlTokenDecoded != null ? Encoding.UTF8.GetString(urlTokenDecoded) : string.Empty;
    }

    public static string GetEncryptedToken<T>(T token) where T : class
    {
      return GetEncryptedString(JsonConvert.SerializeObject(token));
    }
    public static string GetEncryptedString(string str)
    {
      return
        Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(str),
          Encoding.UTF8.GetBytes(AppConstants.TEST_ENCRYPT_KEY)));
    }

    public static string GetDecryptedString(string str)
    {
      return
        Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(str),
          Encoding.UTF8.GetBytes(AppConstants.TEST_ENCRYPT_KEY)));
    }

    public static T GetDecryptedToken<T>(string str) where T : class
    {
      return JsonConvert.DeserializeObject<T>(
        Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(str),
          Encoding.UTF8.GetBytes(AppConstants.TEST_ENCRYPT_KEY))));
    }

    public static byte[] Encrypt(byte[] value, byte[] password)
    {
      byte[] result;
      var strBytes = value;
      var passwordHashBytes = MD5.Create().ComputeHash(password);

      using (var aes = Rijndael.Create())
      {
        aes.IV = passwordHashBytes;
        aes.Key = passwordHashBytes;
        using (var encryptor = aes.CreateEncryptor())
        {
          using (var to = new MemoryStream())
          {
            using (var writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
            {
              writer.Write(strBytes, 0, strBytes.Length);
              writer.FlushFinalBlock();
              result = to.ToArray();
            }
          }
        }

      }
      return result;
    }

    public static byte[] Decrypt(byte[] value, byte[] password)
    {
      byte[] result;
      var strBytes = value;
      var passwordHashBytes = MD5.Create().ComputeHash(password);

      using (var aes = Rijndael.Create())
      {
        aes.IV = passwordHashBytes;
        aes.Key = passwordHashBytes;
        using (var decryptor = aes.CreateDecryptor())
        {
          using (var to = new MemoryStream())
          {
            using (var writer = new CryptoStream(to, decryptor, CryptoStreamMode.Write))
            {
              writer.Write(strBytes, 0, strBytes.Length);
              writer.FlushFinalBlock();
              result = to.ToArray();
            }
          }
        }

      }
      return result;
    }

    public static string SerializeToJson<T>(T obj)
    {
      return JsonConvert.SerializeObject(obj);
    }

    public static T DeserializeFromJson<T>(string obj)
    {
      return JsonConvert.DeserializeObject<T>(obj);
    }

    public static Dictionary<string, object> Flatten(string json)
    {
      var result = new Dictionary<string, object>();
      var output = JsonConvert.DeserializeObject<ExpandoObject>(json);
      GenerateDictionary(output, result, "");
      return result;
    }
    private static void GenerateDictionary(ExpandoObject inoutExpandoObject, Dictionary<string, object> resultDictionary, string parent)
    {
      foreach (var v in inoutExpandoObject)
      {
        var strKey = parent + v.Key;
        var obj = v.Value;

        var expandoObject = obj as ExpandoObject;
        if (expandoObject != null)
        {
          GenerateDictionary(expandoObject, resultDictionary, strKey + ".");
        }
        else
        {
          if (!resultDictionary.ContainsKey(strKey))
          {
            resultDictionary.Add(strKey, obj);
          }
        }
      }
    }
  }
}