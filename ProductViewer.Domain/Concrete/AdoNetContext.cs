using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ProductViewer.Domain.Entities;

namespace ProductViewer.Domain.Concrete
{
    public class AdoNetContext : IDisposable
    {
        private DataSet _productsDataSet;
        private DataSet _producrDescriptionsDataSet;
        private DataSet _productInventoriesDataSet;
        private DataSet _productListPriceHistoriesDataSet;
        private DataSet _productModelsDataSet;
        private DataSet _productModelProductDescriptionCultureDataSet;

        private DbDataAdapter _productsAdapter;
        private DbDataAdapter _productDescriptionsAdapter;
        private DbDataAdapter _productInventoriesAdapter;
        private DbDataAdapter _productListPriceHistoriesAdapter;
        private DbDataAdapter _productModelsAdapter;
        private DbDataAdapter _productModelProductDescriptionCulturesAdapter;

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
            _productModelsDataSet = new DataSet();
            _productModelProductDescriptionCultureDataSet = new DataSet();

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
            _productDescriptionsAdapter = _factory.CreateDataAdapter();
            DbCommand command1 = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "SELECT * FROM Production.ProductDescription";
            _productDescriptionsAdapter.SelectCommand = command;
            _productDescriptionsAdapter.Fill(_producrDescriptionsDataSet, "ProductDescription");

            // _productInventoriesAdapter
            _productInventoriesAdapter = _factory.CreateDataAdapter();
            DbCommand command2 = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "SELECT * FROM Production.ProductInventory WHERE LocationID = 1";
            _productInventoriesAdapter.SelectCommand = command;
            _productInventoriesAdapter.Fill(_productInventoriesDataSet, "ProductInventory");

            // _productListPriceHistoriesAdapter
            _productListPriceHistoriesAdapter = _factory.CreateDataAdapter();
            DbCommand command3 = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "SELECT * FROM Production.ProductListPriceHistory";
            _productListPriceHistoriesAdapter.SelectCommand = command;
            _productListPriceHistoriesAdapter.Fill(_productListPriceHistoriesDataSet, "ProductListPriceHistory");

            // _productModelsAdapter
            _productModelsAdapter = _factory.CreateDataAdapter();
            DbCommand command4 = _factory.CreateCommand();
            command4.Connection = _connection;
            command4.CommandText = "SELECT * FROM Production.ProductModel";
            _productModelsAdapter.SelectCommand = command4;
            _productModelsAdapter.Fill(_productModelsDataSet, "ProductModel");

            // _productModelProductDescriptionCulturesAdapter
            _productModelProductDescriptionCulturesAdapter = _factory.CreateDataAdapter();
            DbCommand command5 = _factory.CreateCommand();
            command5.Connection = _connection;
            command5.CommandText = "SELECT * FROM Production.ProductModelProductDescriptionCulture WHERE CultureID = 'en'";
            _productModelProductDescriptionCulturesAdapter.SelectCommand = command5;
            _productModelProductDescriptionCulturesAdapter.Fill(_productModelProductDescriptionCultureDataSet,
                "ProductModelProductDescriptionCulture");
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

        public DataTable GetProductModels()
        {
            return _productModelsDataSet.Tables["ProductModel"];
        }

        public DataTable GetProductModelProductDescriptionCulture()
        {
            return _productModelProductDescriptionCultureDataSet.Tables
                ["ProductModelProductDescriptionCulture"];
        }

        public void CommitChanges()
        {
            InitialCommands();
            _productModelProductDescriptionCulturesAdapter.Update(_productModelProductDescriptionCultureDataSet, "ProductModelProductDescriptionCulture");
            _productDescriptionsAdapter.Update(_producrDescriptionsDataSet, "ProductDescription");
            _productInventoriesAdapter.Update(_productInventoriesDataSet, "ProductInventory");
            _productListPriceHistoriesAdapter.Update(_productListPriceHistoriesDataSet, "ProductListPriceHistory");
            _productsAdapter.Update(_productsDataSet, "Product");
            _productModelsAdapter.Update(_productModelsDataSet, "ProductModel");
        }

        private void InitialCommands()
        {
            DbCommand command;

            #region _productsAdapter Commands
            // _productsAdapter INSERT command
            command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "INSERT INTO Production.Product " +
                                  "(Name, ProductNumber, SafetyStockLevel, " +
                                  "ReorderPoint, StandardCost, ListPrice, " +
                                  "DaysToManufacture, SellStartDate, ProductModelID) " +
                                  "VALUES (@Name, @ProductNumber, @SafetyStockLevel, " +
                                  "@ReorderPoint, @StandardCost, @ListPrice, " +
                                  "@DaysToManufacture, @SellStartDate, @ProductModelID)";
            DbParameter newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@Name";
            newParameter.DbType = DbType.StringFixedLength;
            newParameter.Size = 50;
            newParameter.SourceColumn = "Name";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductNumber";
            newParameter.DbType = DbType.StringFixedLength;
            newParameter.Size = 25;
            newParameter.SourceColumn = "ProductNumber";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@SafetyStockLevel";
            newParameter.DbType = DbType.Int16;
            newParameter.SourceColumn = "SafetyStockLevel";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ReorderPoint";
            newParameter.DbType = DbType.Int16;
            newParameter.SourceColumn = "ReorderPoint";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@StandardCost";
            newParameter.DbType = DbType.Decimal;
            newParameter.SourceColumn = "StandardCost";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ListPrice";
            newParameter.DbType = DbType.Decimal;
            newParameter.SourceColumn = "ListPrice";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@DaysToManufacture";
            newParameter.DbType = DbType.Int64;
            newParameter.SourceColumn = "DaysToManufacture";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@SellStartDate";
            newParameter.DbType = DbType.DateTime;
            newParameter.SourceColumn = "SellStartDate";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductModelID";
            newParameter.DbType = DbType.Int64;
            newParameter.SourceColumn = "ProductModelID";
            newParameter.IsNullable = true;
            command.Parameters.Add(newParameter);
            _productsAdapter.InsertCommand = command;
            // _productsAdapter DELETE command
            command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "DELETE FROM Production.Product WHERE ProductID = @ProductID";
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductID";
            newParameter.DbType = DbType.Int32;
            newParameter.SourceColumn = "ProductID";
            command.Parameters.Add(newParameter);
            _productsAdapter.DeleteCommand = command;
            // _productsAdapter UPDATE command
            command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "UPDATE Production.Product " +
                                  "SET Name = @Name, ProductNumber = @ProductNumber, " +
                                  "SafetyStockLevel = @SafetyStockLevel, ReorderPoint = @ReorderPoint, " +
                                  "StandardCost = @StandardCost, ListPrice = @ListPrice, " +
                                  "DaysToManufacture = @DaysToManufacture, SellStartDate = @SellStartDate, " +
                                  "ProductModelID = @ProductModelID " +
                                  "WHERE ProductID = @ProductID";
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductID";
            newParameter.DbType = DbType.StringFixedLength;
            newParameter.Size = 50;
            newParameter.SourceColumn = "ProductID";
            command.Parameters.Add(newParameter);
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@Name";
            newParameter.DbType = DbType.StringFixedLength;
            newParameter.Size = 50;
            newParameter.SourceColumn = "Name";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductNumber";
            newParameter.DbType = DbType.StringFixedLength;
            newParameter.Size = 25;
            newParameter.SourceColumn = "ProductNumber";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@SafetyStockLevel";
            newParameter.DbType = DbType.Int16;
            newParameter.SourceColumn = "SafetyStockLevel";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ReorderPoint";
            newParameter.DbType = DbType.Int16;
            newParameter.SourceColumn = "ReorderPoint";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@StandardCost";
            newParameter.DbType = DbType.Decimal;
            newParameter.SourceColumn = "StandardCost";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ListPrice";
            newParameter.DbType = DbType.Decimal;
            newParameter.SourceColumn = "ListPrice";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@DaysToManufacture";
            newParameter.DbType = DbType.Int64;
            newParameter.SourceColumn = "DaysToManufacture";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@SellStartDate";
            newParameter.DbType = DbType.DateTime;
            newParameter.SourceColumn = "SellStartDate";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductModelID";
            newParameter.DbType = DbType.Int64;
            newParameter.SourceColumn = "ProductModelID";
            newParameter.IsNullable = true;
            command.Parameters.Add(newParameter);
            _productsAdapter.UpdateCommand = command;
            #endregion

            #region _productDescriptionsAdapter Commands
            // _productDescriptionsAdapter INSERT Command
            command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "INSERT INTO Production.ProductDescription (Description) " +
                                  "VALUES (@Description)";
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@Description";
            newParameter.DbType = DbType.StringFixedLength;
            newParameter.Size = 400;
            newParameter.SourceColumn = "Description";
            command.Parameters.Add(newParameter);
            _productDescriptionsAdapter.InsertCommand = command;
            // _productDescriptionsAdapter DELETE Command
            command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "DELETE FROM Production.ProductDescription " +
                                  "WHERE ProductDescriptionID = @ProductDescriptionID";
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductDescriptionID";
            newParameter.DbType = DbType.Int32;
            newParameter.SourceColumn = "ProductDescriptionID";
            command.Parameters.Add(newParameter);
            _productDescriptionsAdapter.DeleteCommand = command;
            // _productDescriptionsAdapter UPDATE Command
            command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "UPDATE Production.ProductDescription " +
                                  "SET Description = @Description " +
                                  "WHERE ProductDescriptionID = @ProductDescriptionID";
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@Description";
            newParameter.DbType = DbType.StringFixedLength;
            newParameter.Size = 400;
            newParameter.SourceColumn = "Description";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductDescriptionID";
            newParameter.DbType = DbType.Int32;
            newParameter.SourceColumn = "ProductDescriptionID";
            command.Parameters.Add(newParameter);
            _productDescriptionsAdapter.UpdateCommand = command;
            #endregion

            #region _productInventoriesAdapter Commands
            // _productInventoriesAdapter INSERT Command
            command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "INSERT INTO Production.ProductInventory (ProductID, LocationID, Shelf, Bin, Quantity)" +
                                  "VALUES (@ProductID, '1', @Shelf, @Bin, @Quantity)";
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductID";
            newParameter.DbType = DbType.Int32;
            newParameter.SourceColumn = "ProductID";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@Shelf";
            newParameter.DbType = DbType.StringFixedLength;
            newParameter.Size = 10;
            newParameter.SourceColumn = "Shelf";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@Bin";
            newParameter.DbType = DbType.Byte;
            newParameter.SourceColumn = "Bin";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@Quantity";
            newParameter.DbType = DbType.Int16;
            newParameter.SourceColumn = "Quantity";
            command.Parameters.Add(newParameter);
            _productInventoriesAdapter.InsertCommand = command;
            // _productInventoriesAdapter DELETE Command
            command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "DELETE FROM Production.ProductInventory " +
                                  "WHERE ProductID = @ProductID AND LocationID = '1'";
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductID";
            newParameter.DbType = DbType.Int32;
            newParameter.SourceColumn = "ProductID";
            command.Parameters.Add(newParameter);
            _productInventoriesAdapter.DeleteCommand = command;
            // _productInventoriesAdapter UPDATE Command
            command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "UPDATE Production.ProductInventory " +
                                  "SET ProductID = @ProductID, LocationID = 'en', Shelf = @Shelf, Bin = @Bin, Quantity = @Quantity" +
                                  "WHERE ProductID = @ProductID AND LocationID = '1'";
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductID";
            newParameter.DbType = DbType.Int32;
            newParameter.SourceColumn = "ProductID";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@Shelf";
            newParameter.DbType = DbType.StringFixedLength;
            newParameter.Size = 10;
            newParameter.SourceColumn = "Shelf";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@Bin";
            newParameter.DbType = DbType.Byte;
            newParameter.SourceColumn = "Bin";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@Quantity";
            newParameter.DbType = DbType.Int16;
            newParameter.SourceColumn = "Quantity";
            command.Parameters.Add(newParameter);
            _productInventoriesAdapter.UpdateCommand = command;
            #endregion

            #region _productListPriceHistoriesAdapter Commands
            // _productListPriceHistoriesAdapter INSERT Command
            command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "INSERT INTO Production.ProductListPriceHistory (ProductID, EndDate, StartDate, ListPrice)" +
                                  "VALUES (@ProductID, 'NULL', @StartDate, @ListPrice)";
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductID";
            newParameter.DbType = DbType.Int32;
            newParameter.SourceColumn = "ProductID";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@StartDate";
            newParameter.DbType = DbType.DateTime;
            newParameter.SourceColumn = "StartDate";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ListPrice";
            newParameter.DbType = DbType.Decimal;
            newParameter.SourceColumn = "ListPrice";
            command.Parameters.Add(newParameter);
            _productListPriceHistoriesAdapter.InsertCommand = command;
            // _productListPriceHistoriesAdapter DELETE Command
            command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "DELETE FROM Production.ProductListPriceHistory " +
                                  "WHERE ProductID = @ProductID AND StartDate = @StartDate";
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductID";
            newParameter.DbType = DbType.Int32;
            newParameter.SourceColumn = "ProductID";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@StartDate";
            newParameter.DbType = DbType.DateTime;
            newParameter.SourceColumn = "StartDate";
            command.Parameters.Add(newParameter);
            _productListPriceHistoriesAdapter.DeleteCommand = command;
            // _productListPriceHistoriesAdapter UPDATE Command
            command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "UPDATE Production.ProductListPriceHistory " +
                                  "SET ProductID = @ProductID, EndDate = 'NULL', StartDate = @StartDate, ListPrice = @ListPrice" +
                                  "WHERE ProductID = @ProductID AND StartDate = @StartDate";
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductID";
            newParameter.DbType = DbType.Int32;
            newParameter.SourceColumn = "ProductID";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@StartDate";
            newParameter.DbType = DbType.DateTime;
            newParameter.SourceColumn = "StartDate";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ListPrice";
            newParameter.DbType = DbType.Decimal;
            newParameter.SourceColumn = "ListPrice";
            command.Parameters.Add(newParameter);
            _productListPriceHistoriesAdapter.UpdateCommand = command;
            #endregion

            #region _productModelsAdapter Commands
            // _productModelsAdapter INSERT Command
            command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "INSERT INTO Production.ProductModel (Name)" +
                                  "VALUES (@Name)";
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@Name";
            newParameter.DbType = DbType.StringFixedLength;
            newParameter.Size = 50;
            newParameter.SourceColumn = "Name";
            command.Parameters.Add(newParameter);
            _productModelsAdapter.InsertCommand = command;
            // _productModelsAdapter DELETE Command
            command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "DELETE FROM Production.ProductModel " +
                                  "WHERE ProductModelID = @ProductModelID";
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductModelID";
            newParameter.DbType = DbType.Int32;
            newParameter.SourceColumn = "ProductModelID";
            command.Parameters.Add(newParameter);
            _productModelsAdapter.DeleteCommand = command;
            // _productModelsAdapter UPDATE Command
            command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "UPDATE Production.ProductModel " +
                                  "SET Name = @Name" +
                                  "WHERE ProductModelID = @ProductModelID";
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductModelID";
            newParameter.DbType = DbType.Int32;
            newParameter.SourceColumn = "ProductModelID";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@Name";
            newParameter.DbType = DbType.StringFixedLength;
            newParameter.Size = 50;
            newParameter.SourceColumn = "Name";
            command.Parameters.Add(newParameter);
            _productModelsAdapter.UpdateCommand = command;
            #endregion

            #region _productModelProductDescriptionCulturesAdapter Commands
            // _productModelProductDescriptionCulturesAdapter INSERT Command
            command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "INSERT INTO Production.ProductModelProductDescriptionCulture (ProductModelID, ProductDescriptionID, CultureID)" +
                                  "VALUES (@ProductModelID, @ProductDescriptionID, 'en')";
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductModelID";
            newParameter.DbType = DbType.Int32;
            newParameter.SourceColumn = "ProductModelID";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductDescriptionID";
            newParameter.DbType = DbType.Int32;
            newParameter.SourceColumn = "ProductDescriptionID";
            command.Parameters.Add(newParameter);
            _productModelProductDescriptionCulturesAdapter.InsertCommand = command;
            // _productModelProductDescriptionCulturesAdapter DELETE Command
            command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "DELETE FROM Production.ProductModelProductDescriptionCulture " +
                                  "WHERE ProductModelID = @ProductModelID AND ProductDescriptionID = @ProductDescriptionID AND CultureID = 'en'";
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductModelID";
            newParameter.DbType = DbType.Int32;
            newParameter.SourceColumn = "ProductModelID";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductDescriptionID";
            newParameter.DbType = DbType.Int32;
            newParameter.SourceColumn = "ProductDescriptionID";
            command.Parameters.Add(newParameter);
            _productModelProductDescriptionCulturesAdapter.DeleteCommand = command;
            // _productModelProductDescriptionCulturesAdapter UPDATE Command
            command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "UPDATE Production.ProductModelProductDescriptionCulture " +
                                  "SET ProductModelID = @ProductModelID, ProductDescriptionID = @ProductDescriptionID, CultureID = 'en' " +
                                  "WHERE ProductModelID = @ProductModelID";
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductModelID";
            newParameter.DbType = DbType.Int32;
            newParameter.SourceColumn = "ProductModelID";
            command.Parameters.Add(newParameter);
            //
            newParameter = _factory.CreateParameter();
            newParameter.ParameterName = "@ProductDescriptionID";
            newParameter.DbType = DbType.Int32;
            newParameter.SourceColumn = "ProductDescriptionID";
            command.Parameters.Add(newParameter);
            _productModelProductDescriptionCulturesAdapter.UpdateCommand = command;
            #endregion
        }

        public void RefreshData()
        {
            CommitChanges();
        }

        public void Dispose()
        {
            _productsDataSet?.Dispose();
            _producrDescriptionsDataSet?.Dispose();
            _productInventoriesDataSet?.Dispose();
            _productListPriceHistoriesDataSet?.Dispose();
            _productModelsDataSet?.Dispose();
            _productModelProductDescriptionCultureDataSet?.Dispose();
            _productsAdapter?.Dispose();
            _productDescriptionsAdapter?.Dispose();
            _productInventoriesAdapter?.Dispose();
            _productListPriceHistoriesAdapter?.Dispose();
            _productModelsAdapter?.Dispose();
            _productModelProductDescriptionCulturesAdapter?.Dispose();
            _connection?.Dispose();
        }
    }
}