using Benim.Features.User.Commands;
using Benim.Features.User.Validators;
using FluentValidation.TestHelper;

namespace Benim.UnitTests;

public class UserLoginTests
{
    private LoginUserCommand? _loginUserCommand;
    private readonly LoginUserCommandValidator _validator;

    public UserLoginTests()
    {
        _validator = new LoginUserCommandValidator();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async void Should_Get_Error_When_UserName_Is_NullOrEmpty(string? userName)
    {
        _loginUserCommand = new LoginUserCommand(userName,"1234");
        TestValidationResult<LoginUserCommand> result = await _validator.TestValidateAsync(_loginUserCommand);
        result.ShouldHaveValidationErrorFor(user => user.UserName);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async void Should_Get_Error_When_Password_IsNullOrEmpty(string? password)
    {
        _loginUserCommand = new LoginUserCommand("admin", password);
        TestValidationResult<LoginUserCommand> result = await _validator.TestValidateAsync(_loginUserCommand);
        result.ShouldHaveValidationErrorFor(user => user.Password);
    }

    [Theory]
    [InlineData("1234")]
    [InlineData("admin12")]
    [InlineData("@dmin12")]
    [InlineData("Admin@1")]
    [InlineData("0123456789987654321")]
    [InlineData("Admin!@#0123456789")]
    public async void Should_Get_Error_When_Password_Length_Is_Not_Between8_15(string? password)
    {
        _loginUserCommand = new LoginUserCommand("admin", password);
        TestValidationResult<LoginUserCommand> result = await _validator.TestValidateAsync(_loginUserCommand);
        result.ShouldHaveValidationErrorFor(user => user.Password);
    }

    [Theory]
    [InlineData("1234")]
    [InlineData("admin")]
    [InlineData("@1234")]
    [InlineData("admin1234")]
    [InlineData("A12345678")]
    [InlineData("@dmin1234")]
    public async void Should_Get_Error_When_Password_Is_Not_Match_PasswordRule(string? password)
    {
        _loginUserCommand = new LoginUserCommand("admin", password);
        TestValidationResult<LoginUserCommand> result = await _validator.TestValidateAsync(_loginUserCommand);
        result.ShouldHaveValidationErrorFor(user => user.Password);
    }

    [Theory]
    [InlineData("admin", "Admin@1234")]
    [InlineData("admin", "@dmiN1234")]
    [InlineData("admin", "P@ssword654")]
    public async void Should_Success_When_UserName_And_Password_IsValid(string? userName, string? password)
    {
        _loginUserCommand = new LoginUserCommand(userName, password);
        TestValidationResult<LoginUserCommand> result = await _validator.TestValidateAsync(_loginUserCommand);
        result.ShouldNotHaveValidationErrorFor(user => user.UserName);
        result.ShouldNotHaveValidationErrorFor(user => user.Password);
    }
}