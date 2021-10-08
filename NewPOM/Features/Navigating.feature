Feature: Navigating
	Check if user is able to navigate among unregister permit application pages

@mytag
Scenario: Navigate to SelectPermitType page
	Given I launch the CalculateFeePage
	And I enter vehicleType, subType, address, startDate, duration
		| VehicleType       | SubType | VehicleAddress                         | StartDate  | Duration |
		| Goods carrying vehicle | 2 tonnes or less   | 23 Loris St, SPRINGVALE SOUTH VIC 3172 | 25/10/2021 | 8 days   |
	When I click Next button
	Then The ProgressTitle of next page is displayed
	| PageTitle |
	|Step 2 of 7 : Select permit type|