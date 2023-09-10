namespace TodoWebService.Models.DTOs.Auth
{
    public class AuthTokenDto
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
