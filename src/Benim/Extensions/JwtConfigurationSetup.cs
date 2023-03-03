using Benim.Models;
using Microsoft.Extensions.Configuration;

namespace Benim.Extensions;

public static class JwtConfigurationSetup
{
    public static IServiceCollection AddJwtConfiguration(this IServiceCollection service, IConfiguration configuration)
    {
        service.Configure<JwtConfiguration>(configuration.GetSection(JwtConfiguration.JwtSection));

        return service;
    }
}