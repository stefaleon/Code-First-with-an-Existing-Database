# Code First with an Existing Database
[Entity Framework in Depth: The Complete Guide](https://www.udemy.com/entity-framework-tutorial/)

&nbsp;
## 00 Start the project
* In VS, create a Console Application project.

&nbsp;
## 01 Create a Code First project using an existing Database
* Add a new item by selecting an ADO.NET Entity Data Model and naming it PlutoContext.
* In the Entity Data Model Wizard select Code First from database.
* Select a new connection in order to specify the connection string in the next steps.
* Fill in the database server name, it is .\SQLEXPRESS in my case.
* Select the existing PlutoCodeFirst database (created in the previous exercise).
* The wizard will create a connection string in App.config named PlutoConfig, same as our DbContext.
* Select all tables except \_MigrationHistory and finish.

&nbsp;
## 02 Rename Cours to Course
* In the code created, use VS Rename functionality to correct the badly singuralised Cours to Course.

&nbsp;
## 03 Enable migrations
* In Package Manager Console, enable migrations.
```
PM> enable-migrations
Checking if the context targets an existing database...
Code First Migrations enabled for project Code First with an Existing Database.
```
