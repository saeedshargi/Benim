using TechTalk.SpecFlow;

namespace Benim.AcceptanceTests.Steps;

[Binding]
public class LoginSteps
{

    [Given(@"I have entered invalid UserName: '(.*)' And Password: '(.*)' at the Login page")]
    public void GivenIHaveEnteredInvalidUserNameAndPasswordAtTheLoginPage(string userName, string password)
    {
       
    }

    [When(@"I click the Login button")]
    public async void WhenIClickTheLoginButton()
    {
        
    }

    [Then(@"Should get error '([^']*)' in Login page")]
    public void ThenShouldGetErrorInLoginPage(string message)
    {
        
    }

    [Given(@"I have entered valid UserName: '(.*)' And Password: '(.*)' at the Login page")]
    public void GivenIHaveEnteredValidUserNameAndPasswordAtTheLoginPage(string userName, string password)
    {
        
    }

    [When(@"I click login button")]
    public async void WhenIClickLoginButton()
    {
        
    }

    [Then(@"I should get success result in login")]
    public void ThenIShouldGetSuccessResultInLogin()
    {
        
    }

}