# Introduction

The code has been intentionally written in a messy and suboptimal way to serve as a "test exercise."
This solution is purely for practice and can be modified in any way you see fit. During a potential interview, we will discuss the changes you made and the reasoning behind them.

# What I have changed

## Here’s what I changed:

* Fix obvious bugs like:

  1. doubled routes
  2. Update used id as a list index
  3. Duplicated code
  4. using First to check for null
  5. dead code block
  6. VisualBasic DateTime!
* Use the correct verb \& fix the naming for routes and actions according to REST.
* Implement Result pattern and return proper result status from actions.
* Controllers don't share a single data store, I provided a single data source.
* Add services and move any business logic out of controllers.
* Remove exceptions, There isn't any unexpected behavior.
* Use TimeProvider built-in service for getting date and time.
* Use logger to record useful information and errors in structured logs.
* Use Option pattern for strongly typed configuration.
* Use request/response DTOs and validation.
* Use proper data types. int for Id, decimal for Price \& ...



# Further improvements

With the current in memory data store, there isn't much we can improve on this project. How ever, I liked to write at least one test for the report generator, but since these days I almost always strictly use AI to generate unit tests, it wouldn't show any of my experience.



If we decided to add additional features like persist the data in a file/database or integrate with an external API, we can implement Repository pattern or add EntityFramework or Outbox Pattern with a background process.



I intentionally didn't use Async methods \& CancellationToken because right now, everything is synchronous and it adds unwanted complexity.





