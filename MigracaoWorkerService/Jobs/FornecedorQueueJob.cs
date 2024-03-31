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
    public class FornecedorQueueJob : IJob
    {

        private readonly IFornecedorRepository _repository;
        private readonly ILogger<FornecedorQueueJob> _logger;

        private List<Fornecedor> lista { get; set; }

        private int qtdRegistros { get; set; } = 0;

        private int contador { get; set; } = 0;

        public FornecedorQueueJob(IFornecedorRepository repository,
                                  ILogger<FornecedorQueueJob> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var retorno = _repository.ObterTodos().ToList();
            qtdRegistros = retorno.Count();

            //if (qtdRegistros == 0)
            //{
            //    lista = new List<Fornecedor>()
            //    {
            //        new Fornecedor()
            //        {
            //            RazaoSocial = "José Olímpio",
            //            NomeFantasia = "José Olímpio",
            //            CNPJ = "5698120561289",
            //            IdPessoa = 13
            //        },
            //        new Fornecedor()
            //        {
            //            RazaoSocial = "Maria Rita",
            //            NomeFantasia = "Maria Rita",
            //            CNPJ = "78819156129458",
            //            IdPessoa = 14
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

                    channel.QueueDeclare(queue: "fornecedorQueue",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                    string message = Newtonsoft.Json.JsonConvert.SerializeObject(row);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "fornecedorQueue",
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
