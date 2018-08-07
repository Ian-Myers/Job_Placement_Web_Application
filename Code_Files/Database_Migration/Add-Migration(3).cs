namespace JPDash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBoolCalledJPHiredtoJPStudents : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JPStudents", "JPHired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JPStudents", "JPHired");
        }
    }
}
