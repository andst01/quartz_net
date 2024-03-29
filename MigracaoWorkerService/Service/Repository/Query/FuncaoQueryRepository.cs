using MigracaoWorkerService.Context.Interface;
using MigracaoWorkerService.Service.Models.Query;
using MigracaoWorkerService.Service.Repository.Query.Interface;

namespace MigracaoWorkerService.Service.Repository.Query
{
    public class FuncaoQueryRepository : RepositoryQueryBase<FuncaoQuery>, IFuncaoQueryRepository
    {
        public FuncaoQueryRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
