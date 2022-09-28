using Benim.Domain.Entities;
using Benim.Domain.Interfaces;
using Benim.Exceptions;
using Benim.Features.User.Commands;
using Benim.Infrastructure.Data;
using Benim.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Benim.Infrastructure.Services;

public class UserService: IUserService
{
    private readonly BenimContext _context;
    private readonly UserManager<User<int>> _userManager;
    private readonly RoleManager<Role<int>> _roleManager;
    private readonly SignInManager<User<int>> _signInManager;
    private readonly JwtConfiguration _jwtConfiguration;

    public UserService(BenimContext context, 
        UserManager<User<int>> userManager, 
        RoleManager<Role<int>> roleManager, 
        SignInManager<User<int>> signInManager, 
        IOptions<JwtConfiguration> jwtConfiguration)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _jwtConfiguration = jwtConfiguration.Value;
    }

    public async Task<int> RegisterUserAsync(RegisterUserCommand registerUser)
    {
        var userWithSameEmail = await _userManager.FindByEmailAsync(registerUser.UserName);
        if (userWithSameEmail is not  null)
        {
            throw new BusinessApplicationException("Duplicate User", "A user with this user name already exist!");
        }

        var user = new User<int>().CreateUser(registerUser.UserName,registerUser.Password,registerUser.FirstName,registerUser.LastName,registerUser.BirthDate,registerUser.Email);

        var result = await _userManager.CreateAsync(user, registerUser.Password);
        if (!result.Succeeded) return 0;
        await _userManager.AddToRoleAsync(user, "Admin");
        return user.Id;
    }
}