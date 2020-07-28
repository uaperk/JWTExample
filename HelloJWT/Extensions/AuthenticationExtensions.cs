using HelloJWT.Options;
using HelloJWT.Services.Interfaces;
using HelloJWT.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HelloJWT.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)

        {
            return services.AddSingleton<IAuthenticationService, AuthenticationService>()
                           .Configure<AuthenticationOptions>(configuration.GetSection("AuthenticationOptions"))
                           .Configure<UserOptions>(configuration.GetSection("UserOptions"));
        }

    }
}
