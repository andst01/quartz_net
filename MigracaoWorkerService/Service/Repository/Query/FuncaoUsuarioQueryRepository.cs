using MigracaoWorkerService.Context.Interface;
using MigracaoWorkerService.Service.Models.Query;
using MigracaoWorkerService.Service.Repository.Query.Interface;

namespace MigracaoWorkerService.Service.Repository.Query
{
    public class FuncaoUsuarioQueryRepository : RepositoryQueryBase<FuncaoUsuarioQuery>, IFuncaoUsuarioQueryRepository
    {
        public FuncaoUsuarioQueryRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
