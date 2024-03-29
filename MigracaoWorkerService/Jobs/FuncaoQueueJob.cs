using Microsoft.Extensions.Logging;
using MigracaoWorkerService.Service.Models.Execute;
using MigracaoWorkerService.Service.Repository.Execute.Interface;
using Quartz;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Jobs
{
    public class FuncaoQueueJob : IJob
    {

        private readonly ILogger<FuncaoQueueJob> _logger;
        private readonly IFuncaoRepository _repository;

        public FuncaoQueueJob(ILogger<FuncaoQueueJob> logger,
                              IFuncaoRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public new List<Funcao> lista { get; set; }

        public int contadorGeral { get; set; } = 0;

        public int qtdRegistros { get; set; } = 0;

        public Task Execute(IJobExecutionContext context)
        {

            var retorno = new List<Funcao>();

            if (qtdRegistros == 0)
                retorno = _repository.ObterTodos().ToList();

            qtdRegistros = retorno.Count();

            if (qtdRegistros > contadorGeral)
            {

                var factory = new ConnectionFactory { HostName = "localhost" };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                foreach (var row in retorno)
                {
                    channel.QueueDeclare(queue: "funcaoQueue",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                    string message = Newtonsoft.Json.JsonConvert.SerializeObject(row);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "funcaoQueue",
                        body: body,
                        basicProperties: null);

                    contadorGeral++;
                }
            }

            if (qtdRegistros == contadorGeral)
                context.Scheduler.PauseJob(context.JobDetail.Key, context.CancellationToken);

            return Task.CompletedTask;
        }
    }
}
