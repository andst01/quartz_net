using Microsoft.Extensions.Logging;
using MigracaoWorkerService.JobConfiguration.Contracts;
using MigracaoWorkerService.JobService;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.JobConfiguration
{
    public class JobConfigProvider : IJobConfigProvider
    {

        #region Membros Privados

        // Logger do serviço
        private readonly ILogger<JobSchedulerService> _logger;

        // Conexão com o repositório
        private readonly DbConnection _connection;

        #endregion

        public Task<IList<IJobConfig>> Get(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IJobConfig> GetById(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task LogExecution(string jobName, DateTime dataInicial, DateTime dataFinal, string message, IDictionary<string, KeyValuePair<string, string>> dados)
        {
            throw new NotImplementedException();
        }
    }
}
