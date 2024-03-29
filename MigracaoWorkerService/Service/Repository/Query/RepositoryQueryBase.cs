using MigracaoWorkerService.Context.Interface;
using MigracaoWorkerService.Service.Models.Query;
using MigracaoWorkerService.Service.Repository.Query.Interface;
using MongoDB.Driver;

namespace MigracaoWorkerService.Service.Repository.Query
{
    public class RepositoryQueryBase<T> : IRepositoryQueryBase<T>  where T : Event
    {

        private readonly IMongoDBContext _context;
        protected IMongoCollection<T> collection;

       
        public RepositoryQueryBase(IMongoDBContext context)
        {
            _context = context;
            collection = _context.db.GetCollection<T>(_context.GetCollectionName(typeof(T)));
        }


        public virtual void Add(T obj)
        {
            collection.InsertOne(obj);
        }

        public virtual async Task AddAsync(T obj)
        {
            await collection.InsertOneAsync(obj);
        }

        public IEnumerable<T> GetAll()
        {
            var all = collection.Find(Builders<T>.Filter.Empty);
            return all.ToList();
        }

        public T GetByCode(string campo, string codigo)
        {
            var data = collection.Find(Builders<T>.Filter.Eq(campo, codigo));
            return data.SingleOrDefault();
        }

        public T GetById(string campo, int id)
        {
            var data = collection.Find(Builders<T>.Filter.Eq(campo, id));
            return data.SingleOrDefault();
        }

        public IEnumerable<T> GetByFilter(FilterDefinition<T> filter)
        {
            var data = collection.Find(filter);
            return data.ToEnumerable();
        }

        public virtual void Remove(string campo, string id)
        {
            Task.Run(() => collection.DeleteOneAsync(Builders<T>.Filter.Eq(campo, id)));
        }

        public void RemoveA(string campo, string id)
        {
            collection.DeleteOne(Builders<T>.Filter.Eq(campo, id));
        }

        public virtual void Update(string campo, int id, T obj)
        {
            collection.ReplaceOne(Builders<T>.Filter.Eq(campo, id), obj);
            //await collection.ReplaceOneAsync(x => x._Id == obj._Id, obj);
        }

        public virtual void Update(FilterDefinition<T> filter, T obj)
        {
            var data = collection.Find(filter);
            obj.MongoId = data.FirstOrDefault().MongoId;

            collection.ReplaceOne(filter, obj);
        }

        //public virtual async Task Remove(FilterDefinition<T> filter)
        //{
        //    var data = await collection.FindAsync(filter);
        //    var id = data.FirstOrDefault()._Id;

        //    await collection.DeleteOneAsync(Builders<T>.Filter.Eq(x => x._Id, id));
        //}


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
