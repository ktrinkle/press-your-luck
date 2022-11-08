namespace PressYourLuckApi.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILogger<LoginService> _logger;
        private readonly AppSettings _appSettings;

        public LoginService(ILogger<LoginService> logger, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public async Task<BearerDto?> AdminLoginAsync(AdminLogin userLogin)
        {
            var setPassword = _appSettings.AdminPassword;

            if (userLogin.Password != setPassword)
            {
                return null;
            }

            // we don't care about the team now
            var returnAdmin = new BearerDto()
            {
                UserName = "Administrator",
                BearerToken = await GenerateTokenAsync(Guid.NewGuid(), true, "Administrator")
            };

            return returnAdmin;
        }

        public async Task<BearerDto?> GeekOMaticLoginAsync(string token)
        {
            if (await GetGeekOMaticUserAsync(token))
            {
                return new BearerDto()
                {
                    UserName = "GeekOMatic",
                    BearerToken = await GenerateTokenAsync(Guid.NewGuid(), false, "GeekOMatic")
                };
            }

            return null;
        }

        public async Task<bool> GetGeekOMaticUserAsync(string token)
        {
            var geekOMaticUser = Encoding.UTF8.GetBytes(_appSettings.GeekOMaticUser);
            using var alg = SHA512.Create();

            var hashValue = alg.ComputeHash(geekOMaticUser).Aggregate("", (current, x) => current + $"{x:x2}");

            if (hashValue != token)
            {
                return false;
            }

            return true;
        }

        private async Task<string> GenerateTokenAsync(Guid sessionGuid, bool adminFlag, string? adminUsername)
        {
            var appSecret = Encoding.UTF8.GetBytes(_appSettings.Secret!);
            var appSecurityKey = new SymmetricSecurityKey(appSecret) {KeyId = _appSettings.JWTKeyId};

            var appIssuer = _appSettings.Issuer;
            var appAudience = _appSettings.Audience;

            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim("sessionid", sessionGuid.ToString()),
                new Claim("username", adminUsername ?? ""),
            });

            if (adminFlag)
            {
                claims.AddClaim(new Claim(ClaimTypes.Role, "admin"));
            }

            if (adminUsername == "GeekOMatic")
            {
                claims.AddClaim(new Claim(ClaimTypes.Role, "geekomatic"));
                claims.AddClaim(new Claim("geekomatic", "true"));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(4),
                Issuer = appIssuer,
                Audience = appAudience,
                SigningCredentials = new SigningCredentials(appSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
