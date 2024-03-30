using MigracaoWorkerService.Context;
using MigracaoWorkerService.Service.Models.Execute;
using MigracaoWorkerService.Service.Repository.Execute.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Repository.Execute
{
    public class EnderecoRepository : RepositoryBase<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(SQLContexto db) : base(db)
        {
        }
    }
}
