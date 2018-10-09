using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductViewer.Domain.Abstract;

namespace ProductViewer.Domain.Concrete
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;
        private IDbConnection _connection;

        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
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
    }
}
