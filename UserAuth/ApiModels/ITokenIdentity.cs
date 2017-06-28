using System.Security.Principal;

namespace UserAuth.ApiModels
{
  public interface ITokenIdentity : IIdentity
  {
    string Id { get; }
    string IssuedTo { get; }
    string Scope { get; }
    long Expiry { get; }
    string Version { get; }
  }
}