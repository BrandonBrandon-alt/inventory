using System.Data.SqlClient;
using System.Data.Common;

namespace inventory.Context;

public class SqlServer : IDatabaseContext
    {
        private readonly string _connectionString;
        private SqlConnection _connection;
        public SqlServer(IConfiguration configuration) => _connectionString = configuration.GetConnectionString("SqlServer") ?? throw new ArgumentNullException("SqlServer connection string is missing");
        public void Execute(Action<DbConnection> action)
        {
            using DbConnection connection = CreateConnection();
            connection.Open();
            action(connection);
        }
        public T Execute<T>(Func<DbConnection, T> func)
        {
            using DbConnection connection = CreateConnection();
            connection.Open();
            return func(connection);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
            }
        }
        private DbConnection CreateConnection()
        {
            _connection = new SqlConnection(_connectionString);
            return _connection;
        }
    }

    public interface IDatabaseContext : IDisposable
    {
        void Execute(Action<DbConnection> action);
        T Execute<T>(Func<DbConnection, T> func);
    }
