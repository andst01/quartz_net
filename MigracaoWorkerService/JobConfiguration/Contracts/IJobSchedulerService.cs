using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.JobConfiguration.Contracts
{
    public interface IJobSchedulerService
    {
        /// <summary>
        /// Obtem o flag que informa se a propriedade está sendo executada ou não.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Inicia o agendador de tarefas de geração de arquivo.
        /// </summary>
        Task Start(CancellationToken cancellationToken);

        /// <summary>
        /// Para o agendador de tarefas de geração de arquivo.
        /// </summary>
        Task Stop(CancellationToken cancellationToken);

        /// <summary>
        /// Metodo responsavel por carregar as janelas de processamento no agendador
        /// </summary>
        Task LoadConfigs(IList<IJobConfig> configs, CancellationToken cancellationToken);

    }
}
