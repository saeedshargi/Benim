Feature: Login to application
    As a user
    I want to login to appllication with my user info
    
Scenario Outline: Login with invalid user info
    Given I have entered invalid UserName: '<userName>' And Password: '<password>' at the Login page
    When I click the Login button
    Then Should get error '<message>' in Login page

    Examples: 
    | userName | password | message                       |
    |          |          | Invalid UserName Or Password! |
    | admin    |          | Invalid Password!             |
    |          | 1234     | Invalid UserName!             |
    | user     | 1234     | Invalid UserName Or Password! |
    
Scenario: Login with valid user info
    Given I have entered valid UserName: '<userName>' And Password: '<password>' at the Login page
    When I click login button
    Then I should get success result in login

    Examples: 
    | userName   | password     |
    | admin      | Admin@1234   |