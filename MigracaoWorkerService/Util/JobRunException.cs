using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Util
{
    /// <summary>
    /// Exceptions referentes a execução de tarefas
    /// </summary>
    public class JobRunException : Exception
    {
        /// <summary>
        /// Exceptions referentes ao processo de execução da tarefa
        /// </summary>
        public JobRunException() : base() { }

        /// <summary>
        /// Exceptions referentes ao processo de execução da tarefa
        /// </summary>
        /// <param name="message">Mensagem descritiva referente ao problema ocorrido</param>
        public JobRunException(string message) : base(message) { }

        /// <summary>
        /// Exceptions referentes ao processo de execução da tarefa
        /// </summary>
        /// <param name="message">Mensagem descritiva referente ao problema ocorrido</param>
        /// <param name="innerException">Exception que ocasionou esse problema</param>
        public JobRunException(string message, Exception innerException) : base(message, innerException) { }
    }
}
