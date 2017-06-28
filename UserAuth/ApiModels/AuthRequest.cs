using System;
using System.ComponentModel.DataAnnotations;
using UserAuth.Helpers;

namespace UserAuth.ApiModels
{
  public class AuthRequest
  {
    [Required]
    public string Provider { get; set; }

    [Required]
    public string Token { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    [Required]
    public string EmailId { get; set; }

    public string ProfileData { get; set; }

    public bool IsValid()
    {
      var providerValid = !string.IsNullOrEmpty(Provider)
                          &&
                          (Provider.Equals(AppConstants.ProviderNameSystem, StringComparison.InvariantCultureIgnoreCase)
                           ||
                           Provider.Equals(AppConstants.ProviderNameVerify, StringComparison.InvariantCultureIgnoreCase)
                           ||
                           Provider.Equals(AppConstants.ProviderNameFacebook, StringComparison.InvariantCultureIgnoreCase)
                           ||
                           Provider.Equals(AppConstants.ProviderNameGoogle, StringComparison.InvariantCultureIgnoreCase));
      var tokenValid = !string.IsNullOrEmpty(Token);
      var firstnameValid = FirstName == null || FirstName.Length > 0;
      var lastnameValid = LastName == null || LastName.Length > 0;
      var emailIdValid = !string.IsNullOrEmpty(EmailId);
      var profileDataValid = ProfileData == null || ProfileData.Length > 1;
      return providerValid && tokenValid && firstnameValid && lastnameValid && emailIdValid && profileDataValid;
    }

    public bool ProviderIs(string provider)
    {
      return !string.IsNullOrEmpty(Provider) && Provider.Equals(provider, StringComparison.CurrentCultureIgnoreCase);
    }
  }
}