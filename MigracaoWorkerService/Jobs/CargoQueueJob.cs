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
    public class CargoQueueJob : IJob
    {
        private readonly ICargoRepository _repository;
        private readonly ILogger<CargoQueueJob> _logger;

        public CargoQueueJob(ICargoRepository repository, ILogger<CargoQueueJob> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        private List<Cargo> lista { get; set; }

        private int qtdRegistros { get; set; } = 0;

        private int contador { get; set; } = 0;

        public async Task Execute(IJobExecutionContext context)
        {

            var retorno = _repository.ObterTodos().ToList();
            qtdRegistros = retorno.Count();

            //if(qtdRegistros == 0)
            //{
            //    lista = new List<Cargo>()
            //    {
            //        new Cargo()
            //        {
            //            Descricao = "ATENDENTE",
            //            Ativo = 1,

            //        },
            //        new Cargo()
            //        {
            //            Descricao = "SERVIÇOS GERAIS",
            //            Ativo = 1,

            //        },
            //        new Cargo()
            //        {
            //            Descricao = "ESPECIALISTA I",
            //            Ativo = 1,

            //        },
            //        new Cargo()
            //        {
            //            Descricao = "ESPECIALISTA II",
            //            Ativo = 1,

            //        },
            //        new Cargo()
            //        {
            //            Descricao = "ESPECIALISTA III",
            //            Ativo = 1,

            //        },

            //    };

            //    foreach (var add in lista)
            //    {
            //        await _repository.AdicionarAsync(add);
            //    }

            //    await _repository.Save();
            //}

            if (qtdRegistros > contador)
            {
                foreach (var row in retorno)
                {
                    var factory = new ConnectionFactory { HostName = "localhost" };
                    using var connection = factory.CreateConnection();
                    using var channel = connection.CreateModel();

                    channel.QueueDeclare(queue: "cargoQueue",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                    string message = Newtonsoft.Json.JsonConvert.SerializeObject(row);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "cargoQueue",
                        body: body,
                        basicProperties: null);

                    contador++;

                }
            }

            if (qtdRegistros == contador)
                context.Scheduler.PauseJob(context.JobDetail.Key, context.CancellationToken);

            Task.CompletedTask.Wait();
        }
    }
}
