using MigracaoWorkerService.JobConfiguration.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.JobConfiguration.Modelos
{
    /// <summary>
    /// Objeto representa os triggers da tarefa
    /// </summary>
    public class JobStarter : IJobStarter
    {
        /// <summary>
        /// Obtem ou define o identificador de configuração da tarefa
        /// </summary>
        public int JobConfigId { get; set; }

        /// <summary>
        /// Obtem ou define o identificador da configuração de agendamento da tarefa
        /// </summary>
        public int JobStarterId { get; set; }

        /// <summary>
        /// Obtem ou define a expressão de start do quartz
        /// </summary>
        public string CronExpression { get; set; }
    }
}
