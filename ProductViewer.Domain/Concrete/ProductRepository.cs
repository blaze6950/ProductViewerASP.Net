using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Concrete
{
    public class ProductRepository : IProductsRepository
    {
        private const string SqlGetProductList = "SELECT * FROM Production.Product WHERE ViewStatus = 1";
        private const string SqlGetProduct = "SELECT * FROM Production.Product WHERE ViewStatus = 1 AND ProductID = @ProductID";
        private const string SqlCreate = "dbo.InsertProduct";
        private const string SqlUpdate = "UPDATE Production.Product SET Name = @Name, ProductNumber = @ProductNumber, SafetyStockLevel = @SafetyStockLevel, ReorderPoint = @ReorderPoint, StandardCost = @StandardCost, ListPrice = @ListPrice, DaysToManufacture = @DaysToManufacture, SellStartDate = @SellStartDate, ProductModelID = @ProductModelID WHERE ProductID = @ProductID";
        private const string SqlDelete = "UPDATE Production.Product SET ViewStatus = 0 WHERE ProductID = @ProductID";

        private IDbTransaction _transaction;
        private IDbConnection _connection
        {
            get => _transaction.Connection;
        }

        public ProductRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public IEnumerable<Product> GetProductList()
        {
            var productList = _connection.Query<Product>(SqlGetProductList, transaction: _transaction).ToList();
            return productList;
        }

        public Product GetProduct(int id)
        {
            var product = _connection.QueryFirstOrDefault<Product>(SqlGetProduct, new{ ProductID  = id}, transaction: _transaction);
            return product;
        }

        public Product Create(Product item)
        {
            var p = new DynamicParameters();
            p.Add("@Name", item.Name);
            p.Add("@ProductNumber", item.ProductNumber);
            p.Add("@SafetyStockLevel", item.SafetyStockLevel);
            p.Add("@ReorderPoint", item.ReorderPoint);
            p.Add("@StandardCost", item.StandardCost);
            p.Add("@ListPrice", item.ListPrice);
            p.Add("@DaysToManufacture", item.DaysToManufacture);
            p.Add("@SellStartDate", item.SellStartDate);
            p.Add("@ProductModelID", item.ProductModelID);
            p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var connection = _connection;
            connection.Execute(SqlCreate, p, commandType: CommandType.StoredProcedure, transaction: _transaction);
            item.ProductID = p.Get<int>("@Id");
            return item;
        }

        public void Update(Product item)
        {
            _connection.Execute(SqlUpdate, item, transaction: _transaction);
        }

        public void Delete(int id)
        {
            _connection.Execute(SqlDelete, new {ProductID = id}, transaction: _transaction);
        }
    }
}