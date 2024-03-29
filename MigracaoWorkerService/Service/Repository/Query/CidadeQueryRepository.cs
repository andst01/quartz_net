using MigracaoWorkerService.Context.Interface;
using MigracaoWorkerService.Service.Models.Query;
using MigracaoWorkerService.Service.Repository.Query.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Repository.Query
{
    public class CidadeQueryRepository : RepositoryQueryBase<CidadeQuery>, ICidadeQueryRepository 
    {
        public CidadeQueryRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
