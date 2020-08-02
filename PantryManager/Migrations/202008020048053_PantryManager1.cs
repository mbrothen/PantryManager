namespace PantryManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PantryManager1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        ItemDescription = c.String(),
                        UnitType = c.String(),
                        UnitQty = c.Int(nullable: false),
                        PantryQty = c.Int(nullable: false),
                        ListQty = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Items", new[] { "User_Id" });
            DropTable("dbo.Items");
        }
    }
}
