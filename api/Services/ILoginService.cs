namespace PressYourLuckApi.Services
{
    public interface ILoginService
    {
        Task<BearerDto?> AdminLoginAsync(AdminLogin userName);
        Task<BearerDto?> GeekOMaticLoginAsync(string token);
        Task<bool> GetGeekOMaticUserAsync(string token);
    }
}
