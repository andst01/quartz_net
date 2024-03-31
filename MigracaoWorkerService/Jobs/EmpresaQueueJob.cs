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
    public class EmpresaQueueJob : IJob
    {
        private readonly IEmpresaRepository _repository;
        private readonly ILogger<EmpresaQueueJob> _logger;

        private List<Empresa> lista { get; set; }

        private int qtdRegistros { get; set; } = 0;

        private int contador { get; set; } = 0;

        public EmpresaQueueJob(IEmpresaRepository repository, ILogger<EmpresaQueueJob> logger)
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
            //    lista = new List<Empresa>()
            //    {
            //        new Empresa()
            //        {
            //            CNPJ = "84508176412768",
            //            NomeFantasia = "Produtos Hospitalares S/A",
            //            RazaoSocial = "Produtos Hospitalares S/A",
            //            IdPessoa = 16,
                       
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

                    channel.QueueDeclare(queue: "empresaQueue",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                    string message = Newtonsoft.Json.JsonConvert.SerializeObject(row);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "empresaQueue",
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
