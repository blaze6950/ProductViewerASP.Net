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

        private IDbTransaction _transaction;
        private IDbConnection _connection
        {
            get => _transaction.Connection;
        }

        public ProductDescriptionRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public IEnumerable<ProductDescription> GetProductDescriptionList()
        {
            var productDescriptionList = _connection.Query<ProductDescription>(SqlGetProductDescriptionList, transaction: _transaction).ToList();
            return productDescriptionList;
        }

        public ProductDescription GetProductDescription(int id)
        {
            var productDescription = _connection.QueryFirstOrDefault<ProductDescription>(SqlGetProductDescription, new{ ProductDescriptionID  = id}, transaction: _transaction);
            return productDescription;
        }

        public ProductDescription Create(ProductDescription item)
        {
            var p = new DynamicParameters();
            p.Add("@Description", item.Description);
            p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _connection.Execute(SqlCreate, p, commandType: CommandType.StoredProcedure, transaction: _transaction);
            item.ProductDescriptionID = p.Get<int>("@Id");
            return item;
        }

        public void Update(ProductDescription item)
        {
            _connection.Execute(SqlUpdate, item, transaction: _transaction);
        }

        public void Delete(int id)
        {
            _connection.Execute(SqlDelete, new { ProductDescriptionID = id }, transaction: _transaction);
        }
    }
}