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

&nbsp;
## 04 Add the InitialModel migration (wrong way)
* We can now add the migration.
```
PM> add-migration InitialModel
Scaffolding migration 'InitialModel'.
The Designer Code for this migration file includes a snapshot of your current Code First model. This snapshot is used to calculate the changes to your model when you scaffold the next migration. If you make additional changes to your model that you want to include in this migration, then you can re-scaffold it by running 'Add-Migration InitialModel' again.
```
* The migration added contains code that will try to create tables that already exist because we started with an existing database. This will create an exception if we try to run the migration. To avoid this we need to add the migration using the -IgnoreChanges switch.


&nbsp;
## 05 Add the InitialModel migration (-IgnoreChanges)
* Now we will add the migration again using the -IgnoreChanges switch, as well as the -Force switch in order to have the InitialModel migration recreated.
```
PM> add-migration InitialModel -IgnoreChanges -Force
Re-scaffolding migration 'InitialModel'.
```
This resulted to the creation of an empty migration.
