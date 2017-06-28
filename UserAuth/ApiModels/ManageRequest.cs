using System;
using System.ComponentModel.DataAnnotations;
using UserAuth.Helpers;

namespace UserAuth.ApiModels
{
  public class ManageRequest
  {
    [Required]
    public string Action { get; set; }

    public string Token { get; set; }

    public string Value { get; set; }

    public bool IsValid()
    {
      var actionValid = !string.IsNullOrEmpty(Action) &&
                        (Action.Equals(AppConstants.ActionVerify, StringComparison.InvariantCultureIgnoreCase)
                         || Action.Equals(AppConstants.ActionChangePasword, StringComparison.InvariantCultureIgnoreCase)
                         || Action.Equals(AppConstants.ActionForgotPasword, StringComparison.InvariantCultureIgnoreCase));
      return actionValid;
    }

    public bool ActionIs(string action)
    {
      return !string.IsNullOrEmpty(Action) && Action.Equals(action, StringComparison.InvariantCultureIgnoreCase);
    }
  }
}