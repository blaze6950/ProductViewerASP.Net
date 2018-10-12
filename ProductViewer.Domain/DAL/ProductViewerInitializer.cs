using System;
using System.Collections.Generic;
using System.Data.Entity;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.DAL
{
    class ProductViewerInitializer : CreateDatabaseIfNotExists<ProductViewerContext>
    {
        protected override void Seed(ProductViewerContext context)
        {
            //ProductModels
            var productModels = new List<ProductModel>
            {
                new ProductModel(){Name = "TestProductModelName1", ProductModelID = 1},
                new ProductModel(){Name = "TestProductModelName2", ProductModelID = 2},
                new ProductModel(){Name = "TestProductModelName3", ProductModelID = 3}
            };
            productModels.ForEach(pm => context.ProductModels.Add(pm));
            context.SaveChanges();

            //Products
            var products = new List<Product>
            {
                new Product() {DaysToManufacture = 1, ListPrice = new decimal(11.11), Name = "Test1", ProductID = 1, ProductModelID = 1, ProductNumber = "TT-T001", ReorderPoint = 1, SafetyStockLevel = 1, SellStartDate = DateTime.Today, StandardCost = new decimal(11.11)},
                new Product() {DaysToManufacture = 2, ListPrice = new decimal(22.22), Name = "Test2", ProductID = 2, ProductModelID = 2, ProductNumber = "TT-T002", ReorderPoint = 2, SafetyStockLevel = 2, SellStartDate = DateTime.Today, StandardCost = new decimal(22.22)},
                new Product() {DaysToManufacture = 3, ListPrice = new decimal(33.33), Name = "Test3", ProductID = 3, ProductModelID = 3, ProductNumber = "TT-T003", ReorderPoint = 3, SafetyStockLevel = 3, SellStartDate = DateTime.Today, StandardCost = new decimal(33.33)}
            };
            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();

            //ProductInventories
            var productInventories = new List<ProductInventory>
            {
                new ProductInventory(){Bin = 0, LocationID = 1, Product = products[0], ProductID = 1, Quantity = 1, Shelf = "A"},
                new ProductInventory(){Bin = 1, LocationID = 2, Product = products[1], ProductID = 2, Quantity = 2, Shelf = "B"},
                new ProductInventory(){Bin = 2, LocationID = 3, Product = products[2], ProductID = 3, Quantity = 3, Shelf = "C"},
            };
            productInventories.ForEach(pi => context.ProductInventories.Add(pi));
            context.SaveChanges();

            //ProductListPriceHystories
            var productListPriceHistories = new List<ProductListPriceHistory>
            {
                new ProductListPriceHistory(){ListPrice = (decimal) 11.11, Product = products[0], ProductID = 1, StartDate = DateTime.Today},
                new ProductListPriceHistory(){ListPrice = (decimal) 22.22, Product = products[1], ProductID = 2, StartDate = DateTime.Today},
                new ProductListPriceHistory(){ListPrice = (decimal) 33.33, Product = products[2], ProductID = 3, StartDate = DateTime.Today},
            };
            productListPriceHistories.ForEach(plph => context.ProductListPriceHistories.Add(plph));
            context.SaveChanges();

            //ProductDescriptions
            var productDescriptions = new List<ProductDescription>
            {
                new ProductDescription(){Description = "TestDescription1", ProductDescriptionID = 1},
                new ProductDescription(){Description = "TestDescription2", ProductDescriptionID = 2},
                new ProductDescription(){Description = "TestDescription3", ProductDescriptionID = 3}
            };
            productDescriptions.ForEach(pd => context.ProductDescriptions.Add(pd));
            context.SaveChanges();

            //ProductModelProductDescriptionCultures
            var productModelProductDescriptionCultures = new List<ProductModelProductDescriptionCulture>
            {
                new ProductModelProductDescriptionCulture()
                {
                    CultureID = "en",
                    ProductDescription = productDescriptions[0],
                    ProductDescriptionID = 1,
                    ProductModel = productModels[0],
                    ProductModelID = 1
                },
                new ProductModelProductDescriptionCulture()
                {
                    CultureID = "en",
                    ProductDescription = productDescriptions[1],
                    ProductDescriptionID = 2,
                    ProductModel = productModels[1],
                    ProductModelID = 2
                },
                new ProductModelProductDescriptionCulture()
                {
                    CultureID = "en",
                    ProductDescription = productDescriptions[2],
                    ProductDescriptionID = 3,
                    ProductModel = productModels[2],
                    ProductModelID = 3
                }
            };
            productModelProductDescriptionCultures.ForEach(pmpdc => context.ProductModelProductDescriptionCultures.Add(pmpdc));
            context.SaveChanges();
        }
    }
}
