Feature: FeeApplication
	Check if The Calculate Fee Page of UnRegister Permit Aplication is working

@codeTest
Scenario: Calculate fee for Vehicle type Passenger vehicle and vehicle subType Sendan
	Given I launch the CalculateFeePage
	And I enter vehicleType, subType, address, startDate, duration
		| VehicleType       | SubType | VehicleAddress                         | StartDate  | Duration |
		| Passenger vehicle | Sedan   | 23 Loris St, SPRINGVALE SOUTH VIC 3172 | 25/10/2021 | 8 days   |
	And I click the Calculate button
	Then the permitCost will displayed
		| PermitCost |
		| $56.40     |
	When I click Next button
	And I enter Permit Type Details
	And I click Next button
	And I enter Vehicle Details
	| Make  | Color | YearMade | IdType                              | VehicleId         | Agree |
	| Honda | Black | 2013     | Vehicle Identification Number (VIN) | a1b2c3d4e5f6g7h8m | Y     |
	And I click Next button
	And I enter Enter Individual Application Details
	| FirstName | LastName | ResidentialYes | Phone      | Email                      |
	| Natasha   | Nguyen   | Y              | 0421467325 | quynhnhuhonguyen@gmail.com |
	And I click Next button
	Then The permitFee and tacCharge will displayed
		| PermitFee | TacCharge |
		| $25.60     | $30.80     |
	When I select aggree checkbox
	And I click Next button
	#Then I can enter payment details
	#| CardholderName | CardNumber  | Month | Year | Cvn |
	#| Natasha Nguyen | 12456789012 | 3     | 2027 | 123  |
	#And I can click the ProceedPayment button
