namespace PantryManager.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class categoryenum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "ItemCategory", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.Items", "ItemCategory", c => c.String());
        }
    }
}
