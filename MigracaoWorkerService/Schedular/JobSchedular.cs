using Microsoft.Extensions.Hosting;
using MigracaoWorkerService.Models;
using Quartz.Spi;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MigracaoWorkerService.Service.Repository.Execute.Interface;
using System.Reflection;
using System.Runtime.InteropServices;
using DnsClient;
using MigracaoWorkerService.Jobs;

namespace MigracaoWorkerService.Schedular
{
    public class JobSchedular : IHostedService
    {

        public IScheduler Scheduler { get; set; }
        private readonly IJobFactory jobFactory;
       // private readonly List<JobMetadata> jobMetadatas;
        private readonly ISchedulerFactory schedulerFactory;
        private readonly IQrtzCronExpressionRepository _qrtzCron;

        public JobSchedular(IJobFactory jobFactory, 
                           // List<JobMetadata> jobMetadatas, 
                            ISchedulerFactory schedulerFactory,
                            IQrtzCronExpressionRepository qrtzCron)
        {
            this.jobFactory = jobFactory;
            //this.jobMetadatas = jobMetadatas;
            this.schedulerFactory = schedulerFactory;
            _qrtzCron = qrtzCron;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var jobMetadatas = await _qrtzCron.Listar();

            var tipo = Assembly.LoadWithPartialName("MigracaoWorkerService.Jobs");

            //var tipos =  Assembly.GetEntryAssembly()
            //            .DefinedTypes
            //            .Where(x => x.Name == "CidadeJob")
            //            .FirstOrDefault();
           // var tipo = typeof(CidadeJob);

            var jobs = typeof(IJob);

            Scheduler = await schedulerFactory.GetScheduler();
            Scheduler.JobFactory = jobFactory;

            //var teste = Assembly.LoadWithPartialName("CidadeJob");

           
            //Suporrt for Multiple Jobs
            jobMetadatas?.ForEach(jobMetadata =>
            {
                jobMetadata.JobType = Assembly.GetEntryAssembly()
                        .DefinedTypes
                        .Where(x => x.Name == jobMetadata.JobName)
                        .FirstOrDefault()
                        .AsType();


                //Create Job
                IJobDetail jobDetail = CreateJob(jobMetadata);
                //Create trigger
                ITrigger trigger = CreateTrigger(jobMetadata);
                //Schedule Job
                Scheduler.ScheduleJob(jobDetail, trigger, cancellationToken).GetAwaiter();
                //Start The Schedular
            });
            await Scheduler.Start(cancellationToken);

            //throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private ITrigger CreateTrigger(JobMetadata jobMetadata)
        {
            return TriggerBuilder.Create()
                .WithIdentity(jobMetadata.JobId.ToString())
                .WithCronSchedule(jobMetadata.JobCronExpression)
                .WithDescription(jobMetadata.JobName)
                //.UsingJobData("dataCadastro", DateTime.Now.ToString())
                // .UsingJobData("dataAtualizacao", DateTime.Now.ToString())
                .Build();
        }

        private IJobDetail CreateJob(JobMetadata jobMetadata)
        {
            return JobBuilder.Create(jobMetadata.JobType)
                .WithIdentity(jobMetadata.JobId.ToString())
                .WithDescription(jobMetadata.JobName)
                .UsingJobData("dataCadastro", DateTime.Now.ToString())
                .UsingJobData("dataAtualizacao", DateTime.Now.ToString())
                .Build();
        }
    }
}
