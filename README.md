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


&nbsp;
## 06 Run the migration
* We can only have one pending migration at a time. So, before making any changes to the model, we must run this empty migration on the database, so that Entity Framework will be able to keep track of the changes.
```
PM> update-database
Specify the '-Verbose' flag to view the SQL statements being applied to the target database.
Applying explicit migrations: [201705241525326_InitialModel].
Applying explicit migration: 201705241525326_InitialModel.
Running Seed method.
```

&nbsp;
## 07 Add a new class
* We now may start making changes to the model. We will add a class named Category.
```
public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
```
* Before we add the migration, we need to update our DbContext. So in PlutoContext we will add the Categories DbSet.
```
public virtual DbSet<Category> Categories { get; set; }
```
* Add the migration.
```
PM> add-migration AddCategoriesTable
Scaffolding migration 'AddCategoriesTable'.
The Designer Code for this migration file includes a snapshot of your current Code First model. This snapshot is used to calculate the changes to your model when you scaffold the next migration. If you make additional changes to your model that you want to include in this migration, then you can re-scaffold it by running 'Add-Migration AddCategoriesTable' again.
```

&nbsp;
## 08 Use the Sql() method to populate the Categories table
* The AddCategoriesTable migration will try to create the Categories table in the database. We can use the Sql() method to polulate new tables with some data. In the migration code we first set the identity for Id to false:
```
Id = c.Int(nullable: false, identity: false)
```
* Then we use the Sql() method to add data for Ids and Names.
```
Sql("INSERT INTO Categories VALUES (1, 'Web Development')");
Sql("INSERT INTO Categories VALUES (2, 'Programming Languages')");
Sql("INSERT INTO Categories VALUES (3, 'Frameworks')");
```
* Then we can run the migration and the Categories table and its data can be seen in MSSMS.
```
PM> update-database
Specify the '-Verbose' flag to view the SQL statements being applied to the target database.
Applying explicit migrations: [201705241620382_AddCategoriesTable].
Applying explicit migration: 201705241620382_AddCategoriesTable.
Running Seed method.
```

&nbsp;
## 09 Add the Category property to the Course class
* In the Course class, we can create a Category property and name it Category.
```
public Category Category { get; set; }
```
* Then add the relevant migration.
```
PM> add-migration AddCategoryColumnToCoursesTable
Scaffolding migration 'AddCategoryColumnToCoursesTable'.
The Designer Code for this migration file includes a snapshot of your current Code First model. This snapshot is used to calculate the changes to your model when you scaffold the next migration. If you make additional changes to your model that you want to include in this migration, then you can re-scaffold it by running 'Add-Migration AddCategoryColumnToCoursesTable' again.
```

&nbsp;
## 10 Use the Sql() method to assign a Category
* Now we can use the Sql() method to populate the Category for existing courses. For instance, we can assign all courses to Web Development by adding the following line in the migration code:
```
Sql("UPDATE Courses SET Category_Id = 1");
```
* Then we can run the migration and see the results on Courses in MSSMS.
```
PM> update-database
Specify the '-Verbose' flag to view the SQL statements being applied to the target database.
Applying explicit migrations: [201705250754528_AddCategoryColumnToCoursesTable].
Applying explicit migration: 201705250754528_AddCategoryColumnToCoursesTable.
Running Seed method.
```

&nbsp;
## 11 Add another property to the Course class
* In the Course class, we can create a DateTime property and name it DatePublished. We add the question mark after the type in order to ake it nullable.
```
public DateTime? DatePublished { get; set; }
```
* Then add the relevant migration.
```
PM> add-migration AddDatPublishedColumnToCoursesTable
Scaffolding migration 'AddDatPublishedColumnToCoursesTable'.
The Designer Code for this migration file includes a snapshot of your current Code First model. This snapshot is used to calculate the changes to your model when you scaffold the next migration. If you make additional changes to your model that you want to include in this migration, then you can re-scaffold it by running 'Add-Migration AddDatPublishedColumnToCoursesTable' again.
```
* Then we can run the migration and see the nullable DatePublished column in the Courses table in MSSMS.
```
PM> update-database
Specify the '-Verbose' flag to view the SQL statements being applied to the target database.
Applying explicit migrations: [201705250829269_AddDatPublishedColumnToCoursesTable].
Applying explicit migration: 201705250829269_AddDatPublishedColumnToCoursesTable.
Running Seed method.
```

&nbsp;
## 12 Rename a property in the Course class
* In the Course class, we will rename the Title property to Name, using the renaming functionality of VS in order to update all references.
```
public string Name { get; set; }
```
* Then add the relevant migration.
```
PM> add-migration RenameTitleToNameInCoursesTable
Scaffolding migration 'RenameTitleToNameInCoursesTable'.
The Designer Code for this migration file includes a snapshot of your current Code First model. This snapshot is used to calculate the changes to your model when you scaffold the next migration. If you make additional changes to your model that you want to include in this migration, then you can re-scaffold it by running 'Add-Migration RenameTitleToNameInCoursesTable' again.
```

&nbsp;
## 13 DANGER! Don't drop before setting the data
* This migration is very dangerous because it drops the Title column without setting its data to Name. We must be very careful and fix this before running the migration.
* First set the Name to not nullable, because it doesn't make sense to have a Course without a name.
```
AddColumn("dbo.Courses", "Name", c => c.String(nullable: false));
```
* In order to pass the Title data to Name, we cane use either RenameColumn() and get rid of AddColumn() and DropColumn()...
```
public override void Up()
        {
            RenameColumn("dbo.Courses", "Title", "Name");
        }
```
... or use the Sql() method to populate Name before dropping Title:
```
public override void Up()
        {
            AddColumn("dbo.Courses", "Name", c => c.String(nullable: false));
            Sql("UPDATE Courses SET Name = Title");
            DropColumn("dbo.Courses", "Title");
        }
```
* Make sure that when changes are applied to upgrading code (Up()), the equivalent happens to downgrading code (Down()).
```
public override void Down()
        {
            AddColumn("dbo.Courses", "Title", c => c.String(nullable: false));
            Sql("UPDATE Courses SET Title = Name");
            DropColumn("dbo.Courses", "Name");
        }
```
* Now we can run the migration and see the Name column in the Courses table in MSSMS.
```
PM> update-database
Specify the '-Verbose' flag to view the SQL statements being applied to the target database.
Applying explicit migrations: [201705250841280_RenameTitleToNameInCoursesTable].
Applying explicit migration: 201705250841280_RenameTitleToNameInCoursesTable.
Running Seed method.
```

&nbsp;
## 14 Delete a property in the Course class
* In the Course class, we will delete the DatePublished property.
* Then add the relevant migration.
```
PM> add-migration DeleteDatePublishedColumnFromCoursesTable
Scaffolding migration 'DeleteDatePublishedColumnFromCoursesTable'.
The Designer Code for this migration file includes a snapshot of your current Code First model. This snapshot is used to calculate the changes to your model when you scaffold the next migration. If you make additional changes to your model that you want to include in this migration, then you can re-scaffold it by running 'Add-Migration DeleteDatePublishedColumnFromCoursesTable' again.
```
* And run it.
```
PM> update-database
Specify the '-Verbose' flag to view the SQL statements being applied to the target database.
Applying explicit migrations: [201705250916462_DeleteDatePublishedColumnFromCoursesTable].
Applying explicit migration: 201705250916462_DeleteDatePublishedColumnFromCoursesTable.
Running Seed method.
```

&nbsp;
## 15 Delete a class
* We are going to delete the Category class. First let's remove the Category property from the Course class.
* Then add this migration.
```
PM> add-migration DeleteCategoryColumnFromCoursesTable
Scaffolding migration 'DeleteCategoryColumnFromCoursesTable'.
The Designer Code for this migration file includes a snapshot of your current Code First model. This snapshot is used to calculate the changes to your model when you scaffold the next migration. If you make additional changes to your model that you want to include in this migration, then you can re-scaffold it by running 'Add-Migration DeleteCategoryColumnFromCoursesTable' again.
```
* And then run it.
```
PM> update-database
Specify the '-Verbose' flag to view the SQL statements being applied to the target database.
Applying explicit migrations: [201705252052592_DeleteCategoryColumnFromCoursesTable].
Applying explicit migration: 201705252052592_DeleteCategoryColumnFromCoursesTable.
Running Seed method.
```
* Next we can delete the Category.cs class as well as the Category DbSet in PlutoContext.
* Then add the relevant migration.
```
PM> add-migration DeleteCategoriesTable
Scaffolding migration 'DeleteCategoriesTable'.
The Designer Code for this migration file includes a snapshot of your current Code First model. This snapshot is used to calculate the changes to your model when you scaffold the next migration. If you make additional changes to your model that you want to include in this migration, then you can re-scaffold it by running 'Add-Migration DeleteCategoriesTable' again.
```

&nbsp;
## 16 Maintain the data of the deleted class
* Before dropping the table from the database, we can copy the data to another table in order to keep the data for historical reasons. So we can create the table dbo.\_Categories and copy the data from Categories to it.
```
public override void Up()
        {
            CreateTable(
                "dbo._Categories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            Sql("INSERT INTO _Categories (Name) SELECT Name FROM Categories");

            DropTable("dbo.Categories");
        }
```
* Take care of the Down() method as well.
```
            Sql("INSERT INTO Categories (Name) SELECT Name FROM _Categories");

            DropTable("dbo._Categories");
```
* Now we can run the migration and then see the Categories table gone and replaced by the \_Categories table in MSSMS.
```
PM> update-database
Specify the '-Verbose' flag to view the SQL statements being applied to the target database.
Applying explicit migrations: [201705252100359_DeleteCategoriesTable].
Applying explicit migration: 201705252100359_DeleteCategoriesTable.
Running Seed method.
```

&nbsp;
## 17 Revert to previous state - downgrade the database
* We can checkout to an older version and use a new database by changing the name in the connection string.
* If we need to use the existing database, for instance in order to maintain production data, we can run a migration with targeting to a previous state.
```
PM> update-database -TargetMigration:DeleteDatePublishedColumnFromCoursesTable
Specify the '-Verbose' flag to view the SQL statements being applied to the target database.
Reverting migrations: [201705252100359_DeleteCategoriesTable, 201705252052592_DeleteCategoryColumnFromCoursesTable].
Reverting explicit migration: 201705252100359_DeleteCategoriesTable.
Reverting explicit migration: 201705252052592_DeleteCategoryColumnFromCoursesTable.
```
* Now the database is in the same state it was right after the TargetMigration. We can check out to the relative code version and do the work we need.
