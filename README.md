
 Bookstore related API in .NET Core 2+ WebApi.


This WebApi should mimic a real online bookstore - as in it should be possible to browse the collection of books, add new ones, buy them, browse authors, etc - no need to implement every possible interaction inside a real bookstore but the main ones should be there. 
This WebApi should be implemented using rest api principles - it should have the correct HTTP verbs and the right HTTP responses.
You should use DTO’s instead of database models for interactions with external calls and validate incoming data.


You should implement a Data Access Layer using either EFCore/NHibernate. The database should be SQLServer/MySQL/Firebird, database models and their relationships are left for you to define. You should follow the Code First approach to database design.


You should ensure that, if needed, the logic can be refactored/changed easily. It should use asynchronous programming wherever possible and error handling should be present as well.

Application layers should have a clear distinction between them, and their concerns should be restricted to their own layer, without leaking into other layers.


Application should also have logging implemented and you should use GIT version control and do a lot of small commits instead of a few large ones. You should create the application while keeping in mind the best practices of software development (clean code, SOLID, OOP principles, etc.).
