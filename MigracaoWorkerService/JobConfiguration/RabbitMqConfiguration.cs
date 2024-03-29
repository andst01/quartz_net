using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.JobConfiguration
{
    public class RabbitMqConfiguration
    {
        public string HostQueue { get; set; }
        public string CidaddeQueueName { get; set; }

        public string CidaddeName { get; set; }
    }
}
