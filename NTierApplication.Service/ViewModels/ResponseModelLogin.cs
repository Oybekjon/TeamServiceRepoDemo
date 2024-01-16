namespace NTierApplication.Service.ViewModels;

public class ResponseModelLogin
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; } = null;
    public string TokenType { get; set; }
    public int Expires { get; set; }

}
