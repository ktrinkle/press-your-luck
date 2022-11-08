namespace PressYourLuckApi.Services;

public static class ServicesConfiguration
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        // Services
        services.TryAddScoped<ILoginService, LoginService>();
        services.TryAddScoped<IManageEventService, ManageEventService>();

        // services.TryAddScoped<IClaimsTransformation, AddRolesClaimsTransformation>();
    }
}
