Feature: Add a new hub

A short summary of the feature

@tag1
Scenario: Client adds a new hub
	Given the Client adds new hub (<Name>,<MainContactEmail>,<AlternateEmail>)
	And Hub model is correct
	Then the API should return <StatusCode>
