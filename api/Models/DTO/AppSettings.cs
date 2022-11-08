
namespace PressYourLuckApi.Models
{
    public class AppSettings
    {
        public const string App = "AppSettings";
        public string? Secret { get; set; }
        public string? Salt { get; set; }
        public string Issuer { get; set; } = String.Empty;
        public string Audience { get; set; } = String.Empty;
        public string JWTKeyId { get; set; } = String.Empty;
        public string GeekOMaticUser { get; set; } = String.Empty;
        public string AdminPassword { get; set; }  = String.Empty;
    }
}
