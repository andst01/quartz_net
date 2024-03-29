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
    public class FuncaoUsuarioQueueJob : IJob
    {

        private readonly ILogger<FuncaoUsuarioQueueJob> _logger;
        private readonly IFuncaoUsuarioRepository _repository;

        public int contadorGeral { get; set; } = 0;

        public int qtdRegistros { get; set; } = 0;

        public new List<FuncaoUsuario> lista { get; set; }

        public FuncaoUsuarioQueueJob(ILogger<FuncaoUsuarioQueueJob> logger, 
                                    IFuncaoUsuarioRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var retorno = new List<FuncaoUsuario>();

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
                    channel.QueueDeclare(queue: "funcaoUsuarioQueue",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                    string message = Newtonsoft.Json.JsonConvert.SerializeObject(row);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "funcaoUsuarioQueue",
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
