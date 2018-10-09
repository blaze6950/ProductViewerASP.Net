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

        private IDbTransaction _transaction;
        private IDbConnection _connection
        {
            get => _transaction.Connection;
        }

        public ProductModelRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public IEnumerable<ProductModel> GetProductModelList()
        {
            var productModelList = _connection.Query<ProductModel>(SqlGetProductModelList).ToList();
            return productModelList;
        }

        public ProductModel GetProductModel(int id)
        {
            var productModel = _connection.QueryFirstOrDefault<ProductModel>(SqlGetProductModel, new{ ProductModelID  = id});
            return productModel;
        }

        public ProductModel Create(ProductModel item)
        {
            var p = new DynamicParameters();
            p.Add("@Name", item.Name);
            p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _connection.Execute(SqlCreate, p, commandType: CommandType.StoredProcedure);
            item.ProductModelID = p.Get<int>("@Id");
            return item;
        }

        public void Update(ProductModel item)
        {
            _connection.Execute(SqlUpdate, item);
        }

        public void Delete(int id)
        {
            _connection.Execute(SqlDelete, new { ProductModelID = id });
        }
    }
}