using Microsoft.Extensions.Configuration;
using MigracaoWorkerService.Context.Interface;
using MongoDB.Driver;

namespace MigracaoWorkerService.Context
{
    public class MongoDBContext : IMongoDBContext
    {
        private readonly IConfiguration _configuration;
        public IMongoDatabase db { get; set; }
        public MongoClient _client { get; set; }
        public MongoDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            //_client = new MongoClient(_configuration.GetConnectionString("MongoDBConnection"));
            _client = new MongoClient("mongodb://localhost:27017");
            db = _client.GetDatabase("sgmdb");

        }

        //public MongoClient _client = new MongoClient(_con["MongoDBConnection"].ConnectionString);
       // public IMongoDatabase db { get; set; }

        private readonly List<Func<Task>> _commands;

        public IClientSessionHandle Session { get; set; }

        private void ConfigureMongo()
        {
            if (_client != null)
            {
                return;
            }

            //_client = new MongoClient(_configuration.GetConnectionString("MongoDBConnection"));
            _client = new MongoClient("mongodb://localhost:27017");

            db = _client.GetDatabase("sgmdb");
        }

        public async Task<int> SaveChanges()
        {
            ConfigureMongo();

            using (Session = await _client.StartSessionAsync())
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();
            }

            return _commands.Count;
        }

        public void AddCommand(Func<Task> func)
        {

            _commands.Add(func);
        }

        public string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)

                .FirstOrDefault())?.CollectionName;
        }

        public IMongoCollection<T> GetCollection<T>(string name) where T : class
        {
            ConfigureMongo();


            return db.GetCollection<T>(name);
        }

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }
    }


    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BsonCollectionAttribute : Attribute
    {
        public string CollectionName { get; }
        public BsonCollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
        //public string CollectionName => _collectionName;

        //private static string GetCollectionName()
        //{
        //    return (typeof(T).GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault()
        //        as BsonCollectionAttribute).CollectionName;
        //}


    }
}

