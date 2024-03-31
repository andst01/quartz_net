using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using MigracaoWorkerService.Service.Models.Execute;
using MigracaoWorkerService.Service.Repository.Execute.Interface;
using Quartz;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Jobs
{
    public class ClienteQueueJob : IJob
    {
        private readonly IClienteRepository _repository;
        private readonly ILogger<ClienteQueueJob> _logger;


        private List<Cliente> lista { get; set; }

        private int qtdRegistros { get; set; } = 0;

        private int contador { get; set; } = 0;

        public ClienteQueueJob(IClienteRepository repository, 
                               ILogger<ClienteQueueJob> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var retorno = _repository.ObterTodos().ToList();
            qtdRegistros = retorno.Count();

            //if(qtdRegistros == 0)
            //{
            //    lista = new List<Cliente>()
            //    { 
            //        new Cliente()
            //        {
            //            // Rafel
            //            IdPessoa = 7,
            //            CPF = "56789312456",
            //            Nome = "Rafael Gomes de Oliveira",
            //            DataNascimento = DateTime.Parse("01/01/1982", CultureInfo.GetCultureInfo("pt-br")),
            //            RG = "08561236"
            //        },
            //        new Cliente()
            //        {
            //            // Milton
            //            IdPessoa = 10,
            //            CPF = "56789312456",
            //            Nome = "Milton Gonçalve da Silva",
            //            DataNascimento = DateTime.Parse("01/01/1982", CultureInfo.GetCultureInfo("pt-br")),
            //            RG = "12345678"
            //        }
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

                    channel.QueueDeclare(queue: "clienteQueue",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                    string message = Newtonsoft.Json.JsonConvert.SerializeObject(row);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "clienteQueue",
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
