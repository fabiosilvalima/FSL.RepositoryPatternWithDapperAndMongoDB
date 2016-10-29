using MongoDB.Driver;
using System;

namespace FSL.RepositoryPatternWithDapperAndMongoDB.Repository
{
    /// <summary>
    /// Classe basica de repositorio MONGO DB. Pode ser usada para qualquer implementacao
    /// </summary>
    public class MongoDBRepository : IDisposable
    {
        public MongoDBRepository()
            : this("localhost")
        {

        }

        public MongoDBRepository(string databaseName)
        {
            DatabaseName = databaseName;
            _client = new MongoClient();
            _database = _client.GetDatabase(DatabaseName);
        }

        public void Dispose()
        {
            _client = null;
            _database = null;
        }

        public string DatabaseName { get; set; }
        private IMongoClient _client;
        private IMongoDatabase _database;

        public IMongoDatabase Database
        {
            get
            {
                return _database;
            }
        }
    }
}