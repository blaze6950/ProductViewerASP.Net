using System;
using System.Data;

namespace ProductViewer.Domain.Abstract
{
    public interface IConnectionFactory : IDisposable
    {
        IDbConnection GetConnection { get; }
        IDbTransaction GetTransaction { get; }

        void ResetTransaction();
    }
}