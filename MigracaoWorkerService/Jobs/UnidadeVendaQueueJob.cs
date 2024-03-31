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
    public class UnidadeVendaQueueJob : IJob
    {
        private readonly IUnidadeVendaRepository _repository;
        private readonly ILogger<UnidadeVendaQueueJob> _logger;

        private List<UnidadeVenda> lista { get; set; }

        private int qtdRegistros { get; set; } = 0;

        private int contador { get; set; } = 0;

        public UnidadeVendaQueueJob(IUnidadeVendaRepository repository, 
                                    ILogger<UnidadeVendaQueueJob> logger)
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
            //    lista = new List<UnidadeVenda> 
            //    { 
            //        new UnidadeVenda()
            //        {
            //            IdEmpresa = 1,
            //            IdPessoa = 17,
            //            CNPJ = "85612390561275",
            //            RazaoSocial = "Produstos Hospitalares S/A Filial 1",
            //            NomeFantasia = "Produstos Hospitalares S/A Filial 1",
                        

            //        },
            //        new UnidadeVenda()
            //        {
            //            IdEmpresa = 1,
            //            IdPessoa = 18,
            //            CNPJ = "23794519561734",
            //            RazaoSocial = "Produstos Hospitalares S/A Filial 2",
            //            NomeFantasia = "Produstos Hospitalares S/A Filial 2",
            //        },
            //    };
            //    foreach (var add in lista)
            //    {
            //        await _repository.AdicionarAsync(add);
            //    }

            //   await  _repository.Save();
            //}

            if (qtdRegistros > contador)
            {
                foreach (var row in retorno)
                {
                    var factory = new ConnectionFactory { HostName = "localhost" };
                    using var connection = factory.CreateConnection();
                    using var channel = connection.CreateModel();

                    channel.QueueDeclare(queue: "unidadevendaQueue",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                    string message = Newtonsoft.Json.JsonConvert.SerializeObject(row);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "unidadevendaQueue",
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
