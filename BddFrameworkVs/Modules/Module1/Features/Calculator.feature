Feature: Calculator

Simple calculator for adding two numbers

@mytag
Scenario: Add two numbers
	Given the first number is 50
	And the second number is 70
	When the two numbers are added
	Then the result should be 120

	Scenario: Verify Data Table
		Given the following numbers exist:
			| Number1 | Number2 | Sum  |
			| 10      | 20      | 30   |
			| 15      | 25      | 40   |
			| 5       | 5       | 10   |
	