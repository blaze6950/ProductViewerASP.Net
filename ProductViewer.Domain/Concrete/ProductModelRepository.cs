using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using ProductViewer.Domain.Abstract;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Concrete
{
    public class ProductModelRepository : IProductModelsRepository
    {
        private const string SqlGetProductModelList = "SELECT * FROM Production.ProductModel WHERE ViewStatus = 1";
        private const string SqlGetProductModel = "SELECT * FROM Production.ProductModel WHERE ViewStatus = 1 AND ProductModelID = @ProductModelID";
        private const string SqlCreate = "dbo.InsertProductModel";
        private const string SqlUpdate = "UPDATE Production.ProductModel SET Name = @Name WHERE ProductModelID = @ProductModelID";
        private const string SqlDelete = "UPDATE Production.ProductModel SET ViewStatus = 0 WHERE ProductModelID = @ProductModelID";
        private IConnectionFactory _connectionFactory;

        public ProductModelRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<ProductModel> GetProductModelList()
        {
            var productModelList = _connectionFactory.GetConnection.Query<ProductModel>(SqlGetProductModelList).ToList();
            return productModelList;
        }

        public ProductModel GetProductModel(int id)
        {
            var productModel = _connectionFactory.GetConnection.QueryFirstOrDefault<ProductModel>(SqlGetProductModel, new{ ProductModelID  = id});
            return productModel;
        }

        public ProductModel Create(ProductModel item)
        {
            var p = new DynamicParameters();
            p.Add("@Name", item.Name);
            p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _connectionFactory.GetConnection.Execute(SqlCreate, p, commandType: CommandType.StoredProcedure);
            item.ProductModelID = p.Get<int>("@Id");
            return item;
        }

        public void Update(ProductModel item)
        {
            _connectionFactory.GetConnection.Execute(SqlUpdate, item);
        }

        public void Delete(int id)
        {
            _connectionFactory.GetConnection.Execute(SqlDelete, new { ProductModelID = id });
        }
    }
}