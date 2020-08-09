namespace PantryManager.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class categoryanvalidation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "ItemCategory", c => c.String());
            AlterColumn("dbo.Items", "ItemName", c => c.String(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.Items", "ItemName", c => c.String());
            DropColumn("dbo.Items", "ItemCategory");
        }
    }
}
