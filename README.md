# VicRoads' UnRegister Permit Application Test Automation


## Framework
This coding test automation framework (https://www.vicroads.vic.gov.au/registration/limited-use-permits/unregistered-vehicle-permits/get-an-unregistered-vehicle-permit)

It includes:
* BDD: SpecFlow Version="3.4.31" 
* UI: Page Object Model
* Unit-testing framework: NUnit
* Logging: LogUtil using System.IO
* Browsers: Chrome and FireFox
* Application Configuration: AppConfig.xlm - contains URL, Browser type, log location and all related information relating to application
* Data Driven: MySql and Excel for tradition tests using NUnit. Scenarios outlined from Specflow for BDD
* Extent report: Extent Reports are implemented for both tradition test cases using NUnit and for scenarios outlined using Specflow
* Selenium: 3.141.0 
* Language: C# - Microsoft.NET.Test.Sdk" Version="16.5.0"

## Setup
* install .Net Core 3.1
* install Visual Studio
* install MySql 8.0.26

## What is testing
* To be able to enter data and navigate among pages from page of step 1 to page of step 6 of the given website
* To be able to check some details on the screen of those pages
	> fee cost after clicking the Calculate button on the Calculate Fee of step 1
	> fee details on the Confirm Details page of step 5

## How to Use
### How to execute tests

Running from TextExplorer at of Visual Studio: There are 4 test cases to be run.
* UnitTest1 and UnitTest2: these tests are using framework using NUnit. 
- UnitTest1: VicRoad's Unregister permit application
- UnitTest2: Dummy test to demonstrate Extent Report will produce only 1 report for multi tests
* Fee Application Feature and Navigating Feature: BDD test cases using Specflow


