namespace ProductViewer.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfiguringModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductDescription", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.ProductInventory", "Shelf", c => c.String(nullable: false));
            AlterColumn("dbo.Product", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Product", "ProductNumber", c => c.String(nullable: false));
            AlterColumn("dbo.ProductModel", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductModel", "Name", c => c.String());
            AlterColumn("dbo.Product", "ProductNumber", c => c.String());
            AlterColumn("dbo.Product", "Name", c => c.String());
            AlterColumn("dbo.ProductInventory", "Shelf", c => c.String());
            AlterColumn("dbo.ProductDescription", "Description", c => c.String());
        }
    }
}
