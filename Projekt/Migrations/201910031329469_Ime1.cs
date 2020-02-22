namespace Projekt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ime1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Ime", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.AspNetUsers", "Prezime", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Prezime", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Ime", c => c.String(nullable: false));
        }
    }
}
