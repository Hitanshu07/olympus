using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace UserAuth.ApiModels
{
  public class TokenIdentity : ITokenIdentity
  {
    public TokenIdentity(string id, string issuedTo, string scope, string name, string authenticationType, bool isAuthenticated, long expiry, string version)
    {
      Id = id;
      IssuedTo = issuedTo;
      Scope = scope;
      Name = name;
      AuthenticationType = authenticationType;
      IsAuthenticated = isAuthenticated;
      Expiry = expiry;
      Version = version;
    }

    public string Id { get; private set; }
    public string IssuedTo { get; private set; }
    public string Scope { get; private set; }
    public string Name { get; private set; }
    public string AuthenticationType { get; private set; }
    public bool IsAuthenticated { get; private set; }
    public long Expiry { get; private set; }
    public string Version { get; private set; } 
  }
}