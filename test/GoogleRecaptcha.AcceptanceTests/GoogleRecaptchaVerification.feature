Feature: google recaptcha verification

@mytag
Scenario: Positive google recaptcha verification
	Given I am on google recaptcha test page
	And I receive recaptcha token by clicking tickbox	
	When I click on submit
	Then I should be redirected to protected page with success


