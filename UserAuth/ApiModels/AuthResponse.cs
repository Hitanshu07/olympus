namespace UserAuth.ApiModels
{
  public class AuthResponse
  {
    public string UserId { get; set; }
    public string SystemToken { get; set; }
    public string Message { get; set; }
  }
}