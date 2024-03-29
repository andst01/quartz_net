using Quartz;
using Quartz.Spi;

namespace MigracaoWorkerService.JobFactory
{
    public class JobSchedulerFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;


        public JobSchedulerFactory(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {

            var jobDetail = bundle.JobDetail;
            return (IJob)_serviceProvider.GetService(jobDetail.JobType);

           // return this._serviceProvider.GetService(typeof(IJob)) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }
}
