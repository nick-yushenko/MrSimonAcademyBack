namespace MrSimonAcademy2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserEmail");
        }
    }
}
