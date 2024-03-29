using MigracaoWorkerService.JobConfiguration.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.JobConfiguration.Modelos
{
    public class JobPath : IJobPath
    {
        /// <summary>
        /// Obtem ou define o identificador do path do job
        /// </summary>
        public int JobPathId { get; set; }

        /// <summary>
        /// Obtem ou define o identificador de configuração de job que possui esse path
        /// </summary>
        public int JobConfigId { get; set; }

        /// <summary>
        /// Obtem ou define a tecnologia usada no armazenamento
        /// </summary>
        //public JobPathTech Technology { get; set; }

        /// <summary>
        /// Obtem ou define o path usado pelo Job
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Obtem ou define a conexão do Path
        /// </summary>
        public string PathConnection { get; set; }

        /// <summary>
        /// Obtem ou define o objetivo do Path
        /// </summary>
        // public JobPathObjective Objective { get; set; }
    }
}
