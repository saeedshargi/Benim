using System.Text;
using Benim.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Benim.Extensions;

public static class AuthenticationSetup
{
    public static IServiceCollection AddAuthenticationWithJwt(this IServiceCollection service, IConfiguration configuration)
    {
        var jwtConfig = configuration.GetSection(JwtConfiguration.JwtSection).Get<JwtConfiguration>();

        service.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwtConfig.ValidateIssuer,
                    ValidateAudience = jwtConfig.ValidateAudience,
                    ValidateLifetime = jwtConfig.ValidateLifetime,
                    ValidateIssuerSigningKey = jwtConfig.ValidateIssuerSigningKey,
                    ValidIssuer = jwtConfig.ValidIssuer,
                    ValidAudiences = new List<string> {jwtConfig.ValidAudience},
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.IssuerSigningKey))
                };
            });

        return service;
    }
}