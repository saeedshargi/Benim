using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Benim.Domain.Common;
using Benim.Domain.ValueObjects;
using Benim.Features.Shared;
using Benim.Features.User.Commands;
using Benim.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Benim.Features.User.Handlers;

public class LoginUserHandler: ICommandHandler<LoginUserCommand,Result<LoginResponse>>
{
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly SignInManager<Domain.Entities.User> _signInManager;
    private readonly IOptions<JwtConfiguration> _jwtOptions;

    public LoginUserHandler(UserManager<Domain.Entities.User> userManager, 
        SignInManager<Domain.Entities.User> signInManager, 
        IOptions<JwtConfiguration> jwtOptions)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtOptions = jwtOptions;
    }

    public async Task<Result<LoginResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var existUser = await _userManager.FindByNameAsync(request.UserName);
        if (existUser is null)
        {
            return Result<LoginResponse>.Failure(new Error("User.NotFound","There is no user with this info."));
        }

        var canLogin = await _signInManager.PasswordSignInAsync(request.UserName,request.Password,request.RememberMe,false);
        if (!canLogin.Succeeded)
        {
            return Result<LoginResponse>.Failure(new Error("User.Invalid","Invalid password!"));
        }

        var jwtToken = await GetJwtTokenAsync(existUser);
        var refreshToken = GetRefreshTokenAsync("");
        existUser.UpdateRefreshToken(refreshToken.Token,refreshToken.Expires);
        await _userManager.UpdateAsync(existUser);

        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        var loginResponse = new LoginResponse(existUser.UserName, $"{existUser.FirstName} {existUser.LastName}",
            existUser.Email, token, refreshToken.Token, refreshToken.Expires);
        return Result<LoginResponse>.Success(loginResponse);
    }

    private async Task<JwtSecurityToken> GetJwtTokenAsync(Domain.Entities.User user)
    {
        var jwtSettings = _jwtOptions.Value;
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(t => new Claim("roles", t)).ToList();
        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString())
            }
            .Union(userClaims)
            .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: jwtSettings.ValidIssuer,
            audience: jwtSettings.ValidAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }
    
    private RefreshToken GetRefreshTokenAsync(string ipAddress)
    {
        var jwtSettings = _jwtOptions.Value;
        return new RefreshToken
        {
            Token = RandomTokenString(),
            Expires = DateTime.UtcNow.AddDays(jwtSettings.RefreshTokenDurationInDay),
            Created = DateTime.UtcNow,
            CreatedByIp = ipAddress
        };
    }
    
    private string RandomTokenString()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(128);
        return BitConverter.ToString(randomBytes).Replace("-", "");
    }
}