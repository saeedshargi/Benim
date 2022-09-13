using Benim.Features.User.Commands;
using Benim.Features.User.Handlers;
using TechTalk.SpecFlow;
using Xunit;

namespace Benim.BehaviorTests.Steps
{
    [Binding]
    public class LoginSteps
    {
        private LoginUserCommand? _loginUserCommand;
        private LoginUserHandler? _loginUserHandler;
        private LoginResponse? _loginResponse;

        [Given(@"I have entered invalid UserName: '(.*)' And Password: '(.*)' at the Login page")]
        public void GivenIHaveEnteredInvalidUserNameAndPasswordAtTheLoginPage(string userName, string password)
        {
            _loginUserCommand = new LoginUserCommand(userName, password);
        }

        [When(@"I click the Login button")]
        public async void WhenIClickTheLoginButton()
        {
            _loginUserHandler = new LoginUserHandler();
            _loginResponse = await _loginUserHandler.Handle(_loginUserCommand, new CancellationToken());
        }

        [Then(@"Should get error '([^']*)' in Login page")]
        public void ThenShouldGetErrorInLoginPage(string message)
        {
            Assert.False(_loginResponse.Success);
            Assert.Equal(message,_loginResponse.ToString().Trim());
        }

        [Given(@"I have entered valid UserName: '(.*)' And Password: '(.*)' at the Login page")]
        public void GivenIHaveEnteredValidUserNameAndPasswordAtTheLoginPage(string userName, string password)
        {
            _loginUserCommand = new LoginUserCommand(userName, password);
        }

        [When(@"I click login button")]
        public async void WhenIClickLoginButton()
        {
            _loginUserHandler = new LoginUserHandler();
            _loginResponse = await _loginUserHandler.Handle(_loginUserCommand, new CancellationToken());
        }

        [Then(@"I should get success result in login")]
        public void ThenIShouldGetSuccessResultInLogin()
        {
            Assert.True(_loginResponse.Success);
        }

    }
}
