using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace UserAuth.ApiModels
{
  public class LinkVerifyToken
  {
    public LinkVerifyToken(string tokenId, string emailId, long expiry)
    {
      TokenId = tokenId;
      EmailId = emailId;
      Expiry = expiry;
    }

    public string TokenId { get; private set; }
    public string EmailId { get; private set; }
    public long Expiry { get; private set; }
  }
}