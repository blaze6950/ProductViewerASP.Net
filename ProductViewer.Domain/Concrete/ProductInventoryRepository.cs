using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Concrete
{
    public class ProductInventoryRepository : IProductInventoriesRepository
    {
        private const string SqlGetProductInventoryList = "SELECT * FROM Production.ProductInventory WHERE LocationID = 1";
        private const string SqlGetProductInventory = "SELECT * FROM Production.ProductInventory WHERE LocationID = 1 AND ProductID = @ProductID";
        private const string SqlCreate = "dbo.InsertProductInventory";
        private const string SqlUpdate = "UPDATE Production.ProductInventory SET ProductID = @ProductID, LocationID = 1, Shelf = @Shelf, Bin = @Bin, Quantity = @Quantity WHERE ProductID = @ProductID AND LocationID = 1";
        private const string SqlDelete = "DELETE FROM Production.ProductInventory WHERE ProductID = @ProductID AND LocationID = 1";
        private IConnectionFactory _connectionFactory;

        public ProductInventoryRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<ProductInventory> GetProductInventoryList()
        {
            var productInventoryList = _connectionFactory.GetConnection.Query<ProductInventory>(SqlGetProductInventoryList).ToList();
            return productInventoryList;
        }

        public ProductInventory GetProductInventory(Int16 locationId, int productId)
        {
            var productInventory = _connectionFactory.GetConnection.QueryFirstOrDefault<ProductInventory>(SqlGetProductInventory, new{ProductID = productId});
            return productInventory;
        }

        public ProductInventory Create(ProductInventory item)
        {
            var p = new DynamicParameters();
            p.Add("@ProductID", item.ProductID);
            p.Add("@Shelf", item.Shelf);
            p.Add("@Bin", item.Bin);
            p.Add("@Quantity", item.Quantity);
            _connectionFactory.GetConnection.Execute(SqlCreate, p, commandType: CommandType.StoredProcedure);
            return item;
        }

        public void Update(ProductInventory item)
        {
            _connectionFactory.GetConnection.Execute(SqlUpdate, item);
        }

        public void Delete(Int16 locationId, int productId)
        {
            _connectionFactory.GetConnection.Execute(SqlDelete, new { ProductID = productId });
        }
    }
}