using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Util
{
    /// <summary>
    /// Exceptions referentes ao processo de agendamento
    /// </summary>
    public class JobSchedulerServiceException : Exception
    {
        /// <summary>
        /// Exceptions referentes ao processo de agendamento
        /// </summary>
        public JobSchedulerServiceException() : base() { }

        /// <summary>
        /// Exceptions referentes ao processo de agendamento
        /// </summary>
        /// <param name="message">Mensagem descritiva referente ao problema ocorrido</param>
        public JobSchedulerServiceException(string message) : base(message) { }

        /// <summary>
        /// Exceptions referentes ao processo de agendamento
        /// </summary>
        /// <param name="message">Mensagem descritiva referente ao problema ocorrido</param>
        /// <param name="innerException">Exception que ocasionou esse problema</param>
        public JobSchedulerServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
