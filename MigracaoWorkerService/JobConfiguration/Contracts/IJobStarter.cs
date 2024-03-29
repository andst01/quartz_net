using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.JobConfiguration.Contracts
{
    public interface IJobStarter
    {
        /// <summary>
        /// Obtem ou define a expressão de cronologica para disparar a tarefa.
        /// </summary>
        string CronExpression { get; set; }
    }
}
