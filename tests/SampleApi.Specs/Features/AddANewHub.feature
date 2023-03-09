Feature: Add a new hub

Add a new hub with no initial flights defined

Scenario Outline: Client adds a new hub
	Given the Client adds new hub (<Name>,<MainContactEmail>,<AlternateEmail>) 
	Then the API should return <StatusCode>

Examples: 
	| Name       | MainContactEmail | AlternateEmail      | StatusCode |
	| Test Hub 1 | hub1@example.com | althub1@example.com | 201        |
	| Test Hub 2 | hub2@example.com | althub2@example.com | 201        |
	| Test Hub 3 | hub3@example.com | althub3@example.com | 201        |
	| Test Hub 4 |                  | althub4@example.com | 400        |
	|            | hub5@example.com | althub5@example.com | 500        |
	| Test Hub 6 | hub6@example.com |                     | 201        |
