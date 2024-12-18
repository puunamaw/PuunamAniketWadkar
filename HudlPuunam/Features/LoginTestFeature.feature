Feature: LoginTestFeature

This test covers the login functionality

@puunamTest
Scenario: Login Test with Valid Credentials
	Scenario covers : Happy Path with valid credentials.
	Given I am on the landing page
	When I click the login button and enter credentials
	Then I must be logged in to the application

@puunamTest
Scenario Outline: Login Test with Invalid Username and Password
	Observation : The error text displayed on the password field is not consistent.
	Scenario covers : Testing the login with invalid Uername and Paassword combinations.                 
	Given I am on the landing page
	When I perform login with invalid username "<invalidUsername>"  and passwword "<invalidpassword>"
	Then I must not be logged in to the application or prevented to login
Examples: Invalid Username and Password combinations
	| invalidUsername     | invalidpassword        |
	| test@test.com       | TestAutomationPassword |
	| example@example.com | Example123??$$         |

@puunamTest
Scenario Outline: Login Test with Invalid Username
	Observation : The error text displayed on the password field is not consistent.
	Scenario covers : Testing the login with invalid Uername and Paassword combinations.                 
	Given I am on the landing page
	When I perform login with invalid username "<invalidUsername>"
	Then I must see error displayed for the email field
Examples: Invalid Username and Password combinations
	| invalidUsername        |
	| plainaddress           |
	| @example.com           |
	| user@.com              |
	| user@com               |
	| user@domain..com       |
	| user@domain,com        |
	| user@domain@domain.com |
	| user name@example.com  |
	| username@.com          |
	| username@domain.c      |
	| username@-domain.com   |
	| username@domain-.com   |
	| user#example.com       |
	| username@domain!com    |



@puunamTest
Scenario Outline: Login Test with Invalid Passwords
	Scenario covers : Valid username but invalid password.
	Given I am on the landing page
	When I click the login button and enter invalid passwword as "<invalidpassword>"
	Then I must not be logged in to the application or prevented to login
Examples: Valid username but invalid password.
	| invalidpassword        |
	| TestAutomationPassword |
	| Example123??$$         |

Scenario: Logout
	Given I am on the landing page
	When I click the login button and enter credentials
	Then I must be logged in to the application
	When I click the logout button then I must be logged out

Scenario: Edit Email Address and Login
	Given I am on the landing page
	When I click the login button and enter email address to be edited and reenter valid email and proceed
	Then I must be logged in to the application

Scenario: Forgot Password
	Given I am on the landing page
	When I am on the login page and click forgot password
	Then I must be redirected to Rest Password Page and be able to request a reset password link to my email