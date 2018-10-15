namespace ProductViewer.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductDescription",
                c => new
                    {
                        ProductDescriptionID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProductDescriptionID);
            
            CreateTable(
                "dbo.ProductInventory",
                c => new
                    {
                        ProductID = c.Int(nullable: false),
                        LocationID = c.Short(nullable: false),
                        Shelf = c.String(),
                        Bin = c.Byte(nullable: false),
                        Quantity = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductID, t.LocationID })
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProductNumber = c.String(),
                        SafetyStockLevel = c.Short(nullable: false),
                        ReorderPoint = c.Short(nullable: false),
                        StandardCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ListPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DaysToManufacture = c.Int(nullable: false),
                        SellStartDate = c.DateTime(nullable: false),
                        ProductModelID = c.Int(),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.ProductModel", t => t.ProductModelID)
                .Index(t => t.ProductModelID);
            
            CreateTable(
                "dbo.ProductModel",
                c => new
                    {
                        ProductModelID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ProductModelID);
            
            CreateTable(
                "dbo.ProductListPriceHistory",
                c => new
                    {
                        ProductID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        ListPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ProductID, t.StartDate })
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.ProductModelProductDescriptionCulture",
                c => new
                    {
                        ProductModelID = c.Int(nullable: false),
                        ProductDescriptionID = c.Int(nullable: false),
                        CultureID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ProductModelID, t.ProductDescriptionID, t.CultureID })
                .ForeignKey("dbo.ProductDescription", t => t.ProductDescriptionID, cascadeDelete: true)
                .ForeignKey("dbo.ProductModel", t => t.ProductModelID, cascadeDelete: true)
                .Index(t => t.ProductModelID)
                .Index(t => t.ProductDescriptionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductModelProductDescriptionCulture", "ProductModelID", "dbo.ProductModel");
            DropForeignKey("dbo.ProductModelProductDescriptionCulture", "ProductDescriptionID", "dbo.ProductDescription");
            DropForeignKey("dbo.ProductListPriceHistory", "ProductID", "dbo.Product");
            DropForeignKey("dbo.ProductInventory", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Product", "ProductModelID", "dbo.ProductModel");
            DropIndex("dbo.ProductModelProductDescriptionCulture", new[] { "ProductDescriptionID" });
            DropIndex("dbo.ProductModelProductDescriptionCulture", new[] { "ProductModelID" });
            DropIndex("dbo.ProductListPriceHistory", new[] { "ProductID" });
            DropIndex("dbo.Product", new[] { "ProductModelID" });
            DropIndex("dbo.ProductInventory", new[] { "ProductID" });
            DropTable("dbo.ProductModelProductDescriptionCulture");
            DropTable("dbo.ProductListPriceHistory");
            DropTable("dbo.ProductModel");
            DropTable("dbo.Product");
            DropTable("dbo.ProductInventory");
            DropTable("dbo.ProductDescription");
        }
    }
}
