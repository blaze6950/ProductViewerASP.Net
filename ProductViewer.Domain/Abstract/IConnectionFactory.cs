using System.Data;

namespace ProductViewer.Domain.Abstract
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }
    }
}