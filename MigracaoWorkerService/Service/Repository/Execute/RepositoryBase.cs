using Microsoft.EntityFrameworkCore;
using MigracaoWorkerService.Context;
using MigracaoWorkerService.Service.Repository.Execute.Interface;
using System.Linq.Expressions;

namespace MigracaoWorkerService.Service.Repository.Execute
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly SQLContexto _db;

        public RepositoryBase(SQLContexto db)
        {
            _db = db;
        }

        public T Adicionar(T entity)
        {
            var retorno = _db.Set<T>().Add(entity);
            //var entity1 = retorno.Entity;
            return retorno.Entity;
        }

        public async Task<T> AdicionarAsync(T entity)
        {
            var retorno = await _db.Set<T>().AddAsync(entity);
            return retorno.Entity;
        }

        public T Atualizar(T entity)
        {
            _db.Entry<T>(entity).State = EntityState.Modified;
            var retorno = _db.Set<T>().Attach(entity);
            return retorno.Entity;
        }

        public T Atualizar(Expression<Func<T, bool>> predicate, T novosValores)
        {
            var entidade = _db.Set<T>().Where(predicate).FirstOrDefault();
            _db.Entry<T>(entidade).CurrentValues.SetValues(novosValores);
            _db.Entry(entidade).State = EntityState.Modified;

            return entidade;
        }

        //public Task<bool> Commit()
        //{
        //    return await _db.Commit();
        //}

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Excluir(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public IEnumerable<T> ListarPorParametros(Expression<Func<T, bool>> predicate)
        {
            return _db.Set<T>().Where(predicate).ToList();
        }

        public T ObterPorId(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public async Task<T> ObterPorIdAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public T ObterPorParametros(Expression<Func<T, bool>> predicate)
        {
            return _db.Set<T>().Where(predicate).FirstOrDefault();
        }

        public IQueryable<T> ObterTodos()
        {
            return _db.Set<T>(); 
        }

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0;
        }

       
    }
}
