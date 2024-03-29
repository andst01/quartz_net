using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.JobConfiguration.Contracts
{
    public interface IJobPath
    {
        /// <summary>
        /// Obtem ou define a tecnologia usada no armazenamento
        /// </summary>
        // JobPathTech Technology { get; set; }

        /// <summary>
        /// Obtem ou define o path usado pelo Job
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// Obtem ou define a conexão do Path
        /// </summary>
        string PathConnection { get; set; }

        /// <summary>
        /// Obtem ou define o objetivo do Path
        /// </summary>
        //JobPathObjective Objective { get; set; }
    }
}
