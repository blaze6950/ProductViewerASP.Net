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
        private IConnectionFactory _connection;

        public ProductModelProductDescriptionCultureRepository(IConnectionFactory connectionFactory)
        {
            _connection = connectionFactory;
        }

        public IEnumerable<ProductModelProductDescriptionCulture> GetProductModelProductDescriptionCultureList()
        {
            var productModelProductDescriptionCultureList = _connection.GetConnection.Query<ProductModelProductDescriptionCulture>(SqlGetProductModelProductDescriptionCultureList).ToList();
            return productModelProductDescriptionCultureList;
        }

        public ProductModelProductDescriptionCulture GetProductModelProductDescriptionCulture(int productModelId,
            int productDescriptionId)
        {
            var productModelProductDescriptionCulture = _connection.GetConnection.QueryFirstOrDefault<ProductModelProductDescriptionCulture>(SqlGetProductModelProductDescriptionCulture, new{ ProductModelID = productModelId, ProductDescriptionID = productDescriptionId});
            return productModelProductDescriptionCulture;
        }

        public ProductModelProductDescriptionCulture Create(ProductModelProductDescriptionCulture item)
        {
            var p = new DynamicParameters();
            p.Add("@ProductModelID", item.ProductModelID);
            p.Add("@ProductDescriptionID", item.ProductDescriptionID);
            _connection.GetConnection.Execute(SqlCreate, p);
            return item;
        }

        public void Update(ProductModelProductDescriptionCulture item)
        {
            _connection.GetConnection.Execute(SqlUpdate, item);
        }

        public void Delete(int productModelId, int productDescriptionId)
        {
            _connection.GetConnection.Execute(SqlDelete, new { ProductModelID = productModelId, ProductDescriptionID = productDescriptionId });
        }
    }
}