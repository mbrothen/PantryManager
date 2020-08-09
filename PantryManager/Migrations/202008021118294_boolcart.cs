namespace PantryManager.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class boolcart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "InCar", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Items", "InCar");
        }
    }
}
