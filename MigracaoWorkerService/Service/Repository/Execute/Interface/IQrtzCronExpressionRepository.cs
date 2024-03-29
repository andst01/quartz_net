using MigracaoWorkerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Repository.Execute.Interface
{
    public interface IQrtzCronExpressionRepository
    {
        Task<int> Inserir(JobMetadata jobMetadata);

        Task<List<JobMetadata>> Listar();
    }
}
