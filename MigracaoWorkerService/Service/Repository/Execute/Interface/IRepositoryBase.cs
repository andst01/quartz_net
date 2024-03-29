using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Repository.Execute.Interface
{
    public interface IRepositoryBase<T> : IDisposable where T : class
    {
        IQueryable<T> ObterTodos();
        T ObterPorId(int id);
        Task<T> ObterPorIdAsync(int id);
        T Adicionar(T entity);
        T Atualizar(T entity);
        void Excluir(T entity);
        Task<T> AdicionarAsync(T entity);
        //Task<bool> Commit();
        Task<bool> Save();
       
        T ObterPorParametros(Expression<Func<T, bool>> predicate);

        IEnumerable<T> ListarPorParametros(Expression<Func<T, bool>> predicate);

        T Atualizar(Expression<Func<T, bool>> predicate, T novosValores);
    }
}
