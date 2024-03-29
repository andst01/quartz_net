using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.JobConfiguration.Contracts
{
    public interface IJobConfigProvider
    {
        /// <summary>
        /// Obtem a coleção de configurações
        /// </summary>
        /// <returns>Coleção de configurações da tarefa</returns>
        Task<IList<IJobConfig>> Get(CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtem a configuração de acordo com o identificador
        /// </summary>
        /// <param name="id">Identificação da configuração da tarefa</param>
        /// <returns>Configuração da tarefa</returns>
        Task<IJobConfig> GetById(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Registra a execução do job
        /// </summary>
        /// <param name="jobName">Nome do Job</param>
        /// <param name="dataInicial">Data inicial da execução</param>
        /// <param name="dataFinal">Data final da execução</param>
        /// <param name="message">Mensagem a respeito do processmento</param>
        /// <param name="dados">Dados referentes a execução</param>
        Task LogExecution(string jobName, DateTime dataInicial, DateTime dataFinal, string message, IDictionary<string, KeyValuePair<string, string>> dados);
    }
}
