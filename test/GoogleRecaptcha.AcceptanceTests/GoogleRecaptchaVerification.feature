Feature: google recaptcha verification

@mytag
Scenario: Positive google recaptcha verification
	Given I am on google recaptcha test page
	And I tick the recaptcha checkbox
	Then I should receive recaptcha token
	When I click on submit
	Then I should be redirected to protected page with success


