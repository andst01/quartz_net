using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Models
{
    public class JobMetadata
    {
        public int JobId { get; set; }
        public Type JobType { get; set; }
        public string JobName { get; }
        public string JobCronExpression { get; set; }

        public bool JobAtivo { get; set; }

        public DateTime JobDate { get; set; }
        public JobMetadata(int Id, 
            string jobName,
            string cronExpression,
            Type jobType)
        {
            JobId = Id;
            JobName = jobName;
            JobCronExpression = cronExpression;
            JobType = jobType;
        }

        public JobMetadata()
        {
                
        }
    }
}
