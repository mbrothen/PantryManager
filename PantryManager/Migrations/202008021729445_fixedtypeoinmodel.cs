namespace PantryManager.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class fixedtypeoinmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "InCart", c => c.Boolean(nullable: false));
            DropColumn("dbo.Items", "InCar");
        }

        public override void Down()
        {
            AddColumn("dbo.Items", "InCar", c => c.Boolean(nullable: false));
            DropColumn("dbo.Items", "InCart");
        }
    }
}
