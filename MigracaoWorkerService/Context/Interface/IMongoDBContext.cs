using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Context.Interface
{
    public interface IMongoDBContext
    {
        IMongoDatabase db { get; set; }

        Task<int> SaveChanges();

        void AddCommand(Func<Task> func);

        string GetCollectionName(Type documentType);

        IMongoCollection<T> GetCollection<T>(string name) where T : class;


    }
}
