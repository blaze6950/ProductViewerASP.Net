using System;
using System.Data;
using System.Data.Common;

namespace ProductViewer.Domain.Concrete
{
    public class AdoNetContext : IDisposable
    {
        private DataSet _productsDataSet;
        private DataSet _producrDescriptionsDataSet;
        private DataSet _productInventoriesDataSet;
        private DataSet _productListPriceHistoriesDataSet;

        private DbDataAdapter _productsAdapter;
        private DbDataAdapter _producrDescriptionsAdapter;
        private DbDataAdapter _productInventoriesAdapter;
        private DbDataAdapter _productListPriceHistoriesAdapter;

        private String _connectionString;
        private DbProviderFactory _factory;
        private DbConnection _connection;

        public AdoNetContext(string connectionString)
        {
            _connectionString = connectionString;
            var factoryName = "System.Data.SqlClient";
            _factory = DbProviderFactories.GetFactory(factoryName);
            _connection = _factory.CreateConnection();
            if (_connection == null)
            {
                throw new Exception("_connection contain null value");
            }
            _connection.ConnectionString = _connectionString;

            _productsDataSet = new DataSet();
            _producrDescriptionsDataSet = new DataSet();
            _productInventoriesDataSet = new DataSet();
            _productListPriceHistoriesDataSet = new DataSet();

            LoadData();
        }

        private void LoadData()
        {
            // _productsAdapter
            _productsAdapter = _factory.CreateDataAdapter();
            DbCommand command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "SELECT * FROM Production.Product";
            _productsAdapter.SelectCommand = command;
            _productsAdapter.Fill(_productsDataSet, "Product");

            // productDescriptionsAdapter
            _producrDescriptionsAdapter = _factory.CreateDataAdapter();
            DbCommand command1 = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "SELECT * FROM Production.ProductDescription";
            _producrDescriptionsAdapter.SelectCommand = command;
            _producrDescriptionsAdapter.Fill(_producrDescriptionsDataSet, "ProductDescription");

            // _productInventoriesAdapter
            _productInventoriesAdapter = _factory.CreateDataAdapter();
            DbCommand command2 = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "SELECT * FROM Production.ProductInventory";
            _productInventoriesAdapter.SelectCommand = command;
            _productInventoriesAdapter.Fill(_productInventoriesDataSet, "ProductInventory");

            // _productListPriceHistoriesAdapter
            _productListPriceHistoriesAdapter = _factory.CreateDataAdapter();
            DbCommand command3 = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "SELECT * FROM Production.ProductListPriceHistory";
            _productListPriceHistoriesAdapter.SelectCommand = command;
            _productListPriceHistoriesAdapter.Fill(_productListPriceHistoriesDataSet, "ProductListPriceHistory");
        }
        
        public DataTable GetProducts()
        {
            return _productsDataSet.Tables["Product"];
        }

        public DataTable GetProductDescriptions()
        {
            return _producrDescriptionsDataSet.Tables["ProductDescription"];
        }

        public DataTable GetProductInventories()
        {
            return _productInventoriesDataSet.Tables["ProductInventory"];
        }

        public DataTable GetProductListPriceHistories()
        {
            return _productListPriceHistoriesDataSet.Tables["ProductListPriceHistory"];
        }

        public void CommitChanges()
        {
            _productsAdapter.Update(_productsDataSet, "Product");
            _producrDescriptionsAdapter.Update(_producrDescriptionsDataSet, "Productdescription");
            _productInventoriesAdapter.Update(_productInventoriesDataSet, "ProductInventory");
            _productListPriceHistoriesAdapter.Update(_productListPriceHistoriesDataSet, "ProductListPriceHistory");
        }

        public void RefreshData()
        {
            CommitChanges();
        }

        public void Dispose()
        {
            CommitChanges();
            _productsDataSet?.Dispose();
            _producrDescriptionsDataSet?.Dispose();
            _productInventoriesDataSet?.Dispose();
            _productListPriceHistoriesDataSet?.Dispose();
            _productsAdapter?.Dispose();
            _producrDescriptionsAdapter?.Dispose();
            _productInventoriesAdapter?.Dispose();
            _productListPriceHistoriesAdapter?.Dispose();
            _connection?.Dispose();
        }
    }
}