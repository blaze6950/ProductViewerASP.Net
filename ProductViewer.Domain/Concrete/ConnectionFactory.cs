using System.Data;
using System.Data.Common;
using ProductViewer.Domain.Abstract;

namespace ProductViewer.Domain.Concrete
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbTransaction GetTransaction
        {
            get
            {
                if (_transaction == null)
                {
                    _transaction = GetConnection.BeginTransaction();
                }
                return _transaction;
            }
        }

        public void ResetTransaction()
        {
            _transaction = null;
        }

        public IDbConnection GetConnection
        {
            get
            {
                if (_connection == null)
                {
                    var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                    _connection = factory.CreateConnection();
                    if (_connection != null)
                    {
                        _connection.ConnectionString = _connectionString;
                        _connection.Open();
                    }
                    else
                    {
                        throw new DataException("Cannot open connection to DB; _connection is null");
                    }
                }
                return _connection;
            }
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _transaction?.Dispose();
        }
    }
}
