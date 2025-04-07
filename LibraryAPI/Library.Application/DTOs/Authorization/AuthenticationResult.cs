namespace Library.Application.DTOs.Authorization;

public class AuthenticationResult
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }    

    public AuthenticationResult(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}