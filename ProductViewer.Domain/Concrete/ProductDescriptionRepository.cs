using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Concrete
{
    public class ProductDescriptionRepository : IProductDescriptionsRepository
    {
        private const string SqlGetProductDescriptionList = "SELECT * FROM Production.ProductDescription";
        private const string SqlGetProductDescription = "SELECT * FROM Production.ProductDescription WHERE ProductDescriptionID = @ProductDescriptionID";
        private const string SqlCreate = "dbo.InsertProductDescription";
        private const string SqlUpdate = "UPDATE Production.ProductDescription SET Description = @Description WHERE ProductDescriptionID = @ProductDescriptionID";
        private const string SqlDelete = "DELETE FROM Production.ProductDescription WHERE ProductDescriptionID = @ProductDescriptionID";
        private IConnectionFactory _connectionFactory;

        public ProductDescriptionRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<ProductDescription> GetProductDescriptionList()
        {
            var productDescriptionList = _connectionFactory.GetConnection.Query<ProductDescription>(SqlGetProductDescriptionList).ToList();
            return productDescriptionList;
        }

        public ProductDescription GetProductDescription(int id)
        {
            var productDescription = _connectionFactory.GetConnection.QueryFirstOrDefault<ProductDescription>(SqlGetProductDescription, new{ ProductDescriptionID  = id});
            return productDescription;
        }

        public ProductDescription Create(ProductDescription item)
        {
            var p = new DynamicParameters();
            p.Add("@Description", item.Description);
            p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _connectionFactory.GetConnection.Execute(SqlCreate, p, commandType: CommandType.StoredProcedure);
            item.ProductDescriptionID = p.Get<int>("@Id");
            return item;
        }

        public void Update(ProductDescription item)
        {
            _connectionFactory.GetConnection.Execute(SqlUpdate, item);
        }

        public void Delete(int id)
        {
            _connectionFactory.GetConnection.Execute(SqlDelete, new { ProductDescriptionID = id });
        }
    }
}