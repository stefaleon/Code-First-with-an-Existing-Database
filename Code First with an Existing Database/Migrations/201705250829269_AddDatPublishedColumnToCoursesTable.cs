namespace Code_First_with_an_Existing_Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDatPublishedColumnToCoursesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "DatePublished", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "DatePublished");
        }
    }
}
