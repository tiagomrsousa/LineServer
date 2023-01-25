Feature: Get line
As a user 
I want to call my service and retrieve a line

Scenario: Call service with valid index
Given I have a file already read
When I call my service
Then I must retrieve a line