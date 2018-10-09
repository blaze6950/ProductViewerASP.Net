using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Concrete
{
    public class ProductModelProductDescriptionCultureRepository : IProductModelProductDescriptionCulturesRepository
    {
        private const string SqlGetProductModelProductDescriptionCultureList = "SELECT * FROM Production.ProductModelProductDescriptionCulture WHERE CultureID = 'en'";
        private const string SqlGetProductModelProductDescriptionCulture = "SELECT * FROM Production.ProductModelProductDescriptionCulture WHERE ProductModelID = @ProductModelID AND ProductDescriptionID = @ProductDescriptionID AND CultureID = 'en'";
        private const string SqlCreate = "INSERT INTO Production.ProductModelProductDescriptionCulture (ProductModelID, ProductDescriptionID, CultureID) VALUES (@ProductModelID, @ProductDescriptionID, 'en')";
        private const string SqlUpdate = "UPDATE Production.ProductModelProductDescriptionCulture SET ProductModelID = @ProductModelID, ProductDescriptionID = @ProductDescriptionID, CultureID = 'en' WHERE ProductModelID = @ProductModelID AND ProductDescriptionID = @ProductDescriptionID AND CultureID = 'en'";
        private const string SqlDelete = "DELETE FROM Production.ProductModelProductDescriptionCulture WHERE ProductModelID = @ProductModelID AND ProductDescriptionID = @ProductDescriptionID AND CultureID = 'en'";

        private IDbTransaction _transaction;
        private IDbConnection _connection
        {
            get => _transaction.Connection;
        }

        public ProductModelProductDescriptionCultureRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public IEnumerable<ProductModelProductDescriptionCulture> GetProductModelProductDescriptionCultureList()
        {
            var productModelProductDescriptionCultureList = _connection.Query<ProductModelProductDescriptionCulture>(SqlGetProductModelProductDescriptionCultureList).ToList();
            return productModelProductDescriptionCultureList;
        }

        public ProductModelProductDescriptionCulture GetProductModelProductDescriptionCulture(int productModelId,
            int productDescriptionId)
        {
            var productModelProductDescriptionCulture = _connection.QueryFirstOrDefault<ProductModelProductDescriptionCulture>(SqlGetProductModelProductDescriptionCulture, new{ ProductModelID = productModelId, ProductDescriptionID = productDescriptionId});
            return productModelProductDescriptionCulture;
        }

        public ProductModelProductDescriptionCulture Create(ProductModelProductDescriptionCulture item)
        {
            var p = new DynamicParameters();
            p.Add("@ProductModelID", item.ProductModelID);
            p.Add("@ProductDescriptionID", item.ProductDescriptionID);
            _connection.Execute(SqlCreate, p);
            return item;
        }

        public void Update(ProductModelProductDescriptionCulture item)
        {
            _connection.Execute(SqlUpdate, item);
        }

        public void Delete(int productModelId, int productDescriptionId)
        {
            _connection.Execute(SqlDelete, new { ProductModelID = productModelId, ProductDescriptionID = productDescriptionId });
        }
    }
}