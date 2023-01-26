Feature: Get line
As a user 
I want to call my service and retrieve a line

Scenario: Call service with valid index
Given My server is ready
When I call my line service successfully
Then I must retrieve a line