using MigracaoWorkerService.Service.Models.Query;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Repository.Query.Interface
{
    public interface IRepositoryQueryBase<T> where T : Event
    {
        void Add(T obj);

        Task AddAsync(T obj);

        IEnumerable<T> GetAll();

        T GetByCode(string campo, string codigo);

        IEnumerable<T> GetByFilter(FilterDefinition<T> filter);

        void Remove(string campo, string id);

        void Update(string campo, int id, T obj);

        void Update(FilterDefinition<T> filter, T obj);

       // void RemoveA(string campo, string id);
    }
}
