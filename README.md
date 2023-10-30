# What is this?

A project seed for a C# dotnet API ("PaylocityBenefitsCalculator").  It is meant to get you started on the Paylocity BackEnd Coding Challenge by taking some initial setup decisions away.

The goal is to respect your time, avoid live coding, and get a sense for how you work.

# Coding Challenge

**Show us how you work.**

Each of our Paylocity product teams operates like a small startup, empowered to deliver business value in
whatever way they see fit. Because our teams are close knit and fast moving it is imperative that you are able
to work collaboratively with your fellow developers. 

This coding challenge is designed to allow you to demonstrate your abilities and discuss your approach to
design and implementation with your potential colleagues. You are free to use whatever technologies you
prefer but please be prepared to discuss the choices you’ve made. We encourage you to focus on creating a
logical and functional solution rather than one that is completely polished and ready for production.

The challenge can be used as a canvas to capture your strengths in addition to reflecting your overall coding
standards and approach. There’s no right or wrong answer.  It’s more about how you think through the
problem. We’re looking to see your skills in all three tiers so the solution can be used as a conversation piece
to show our teams your abilities across the board.

# Requirements
* Problem statement
  * Implement a backend solution where a user, an employee, has their paychecks calculated for a year based on the following requirements
* How do I get started?
  * Create new repository, seeded from provided zip file
  * Implement requirements
  * Document any decisions that you make with comments explaining "why"
  * Provide us with a link to your code repository
* Requirements
  * able to view employees and their dependents
  * an employee may only have 1 spouse or domestic partner (not both)
  * an employee may have an unlimited number of children
  * able to calculate and view a paycheck for an employee given the following rules:
    * 26 paychecks per year with deductions spread as evenly as possible on each paycheck
    * employees have a base cost of $1,000 per month (for benefits)
    * each dependent represents an additional $600 cost per month (for benefits)
    * employees that make more than $80,000 per year will incur an additional 2% of their yearly salary in benefits costs
    * dependents that are over 50 years old will incur an additional $200 per month
* What are we looking for?
  * understanding of business requirements
  * correct implementation of requirements
  * test coverage for the cost calculations
  * code/architecture quality
  * plan for future flexibility
  * address "task" code comments
  * easy to run your code (if non-standard, provide directions)
* What should you not waste time on?
  * authentication/authorization
  * input sanitization
  * logging
  * adding multiple projects to represent layers... putting everything in the API project is fine
