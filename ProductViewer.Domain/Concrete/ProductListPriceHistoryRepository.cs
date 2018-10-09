using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Concrete
{
    public class ProductListPriceHistoryRepository : IProductListPriceHistoriesRepository
    {
        private const string SqlGetProductListProceHistoryList = "SELECT * FROM Production.ProductListPriceHistory";
        private const string SqlGetProductListProceHistory = "SELECT * FROM Production.ProductListPriceHistory WHERE ProductID = @ProductID AND StartDate = @StartDate";
        private const string SqlCreate = "INSERT INTO Production.ProductListPriceHistory (ProductID, EndDate, StartDate, ListPrice) VALUES (@ProductID, NULL, @StartDate, @ListPrice)";
        private const string SqlUpdate = "UPDATE Production.ProductListPriceHistory SET ProductID = @ProductID, StartDate = @StartDate, ListPrice = @ListPrice WHERE ProductID = @ProductID AND StartDate = @StartDate";
        private const string SqlDelete = "DELETE FROM Production.ProductListPriceHistory WHERE ProductID = @ProductID AND StartDate = @StartDate";

        private IDbTransaction _transaction;
        private IDbConnection _connection
        {
            get => _transaction.Connection;
        }

        public ProductListPriceHistoryRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public IEnumerable<ProductListPriceHistory> GetProductListPriceHistoryList()
        {
            var productListPriceHistoryList = _connection.Query<ProductListPriceHistory>(SqlGetProductListProceHistoryList).ToList();
            return productListPriceHistoryList;
        }

        public ProductListPriceHistory GetProductListPriceHistory(int productId, DateTime startDate)
        {
            var productListPriceHistory = _connection.QueryFirstOrDefault<ProductListPriceHistory>(SqlGetProductListProceHistory, new{ ProductID = productId, StartDate = startDate});
            return productListPriceHistory;
        }

        public ProductListPriceHistory Create(ProductListPriceHistory item)
        {
            var p = new DynamicParameters();
            p.Add("@ProductID", item.ProductID);
            p.Add("@StartDate", item.StartDate);
            p.Add("@ListPrice", item.ListPrice);
            _connection.Execute(SqlCreate, p);
            return item;
        }

        public void Update(ProductListPriceHistory item)
        {
            _connection.Execute(SqlUpdate, item);
        }

        public void Delete(int productId, DateTime startDate)
        {
            _connection.Execute(SqlDelete, new { ProductID = productId, StartDate = startDate });
        }
    }
}