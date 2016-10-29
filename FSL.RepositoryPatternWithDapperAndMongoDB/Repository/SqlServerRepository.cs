using System;
using System.Configuration;
using System.Data.SqlClient;

namespace FSL.RepositoryPatternWithDapperAndMongoDB.Repository
{
    /// <summary>
    /// Classe basica de repositorio SQL Server. Pode ser usada para qualquer implementacao
    /// </summary>
    public class SqlServerRepository : IDisposable
    {
        public SqlServerRepository()
            : this("sqlserver")
        {

        }

        public SqlServerRepository(string connectionStringId)
        {
            _connectionStringId = connectionStringId;
        }

        public virtual string ConnnectionStringId
        {
            get
            {
                return _connectionStringId;
            }
        }

        protected SqlConnection Database
        {
            get
            {
                if (_connection == null)
                {
                    if (string.IsNullOrEmpty(_connectionString))
                    {
                        _connectionString = ConfigurationManager.ConnectionStrings[ConnnectionStringId].ConnectionString;
                    }

                    _connection = new SqlConnection(_connectionString);
                }

                return _connection;
            }
        }

        public SqlServerRepository UseConnectionStringId(string connectionStringId)
        {
            _connectionStringId = connectionStringId;

            return this;
        }

        public SqlServerRepository UseConnectionString(string connectionString)
        {
            _connectionString = connectionString;

            return this;
        }

        public virtual void Dispose()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }

        private SqlConnection _connection = null;
        private string _connectionStringId;
        private string _connectionString;
    }
}