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
    public class FuncionarioQueueJob : IJob
    {
        private readonly IFuncionarioRepository _repository;
        private readonly ILogger<FuncionarioQueueJob> _logger;

        private List<Funcionario> lista { get; set; }

        private int qtdRegistros { get; set; } = 0;

        private int contador { get; set; } = 0;

        public async Task Execute(IJobExecutionContext context)
        {

            var retorno = _repository.ObterTodos().ToList();
            qtdRegistros = retorno.Count();

            if(qtdRegistros == 0)
            {
                lista = new List<Funcionario>()
                {
                    new Funcionario()
                    {
                         IdPessoa = 5,
                        CPF = "5469825281612",
                        Nome = "Alex Xavier da Silva",
                        DataNascimento = DateTime.Parse("17/05/1982", CultureInfo.GetCultureInfo("pt-br")),
                        RG = "97265329"
                    },
                    new Funcionario()
                    {
                         IdPessoa = 8,
                        CPF = "85691573791",
                        Nome = "Ana Beatriz Pacheco",
                        DataNascimento = DateTime.Parse("23/07/1982", CultureInfo.GetCultureInfo("pt-br")),
                        RG = "18256792"
                    },
                };

            }

            //if (qtdRegistros > contador)
            //{
            //    foreach (var row in retorno)
            //    {
            //        var factory = new ConnectionFactory { HostName = "localhost" };
            //        using var connection = factory.CreateConnection();
            //        using var channel = connection.CreateModel();

            //        channel.QueueDeclare(queue: "funcionarioQueue",
            //                durable: false,
            //                exclusive: false,
            //                autoDelete: false,
            //                arguments: null);

            //        string message = Newtonsoft.Json.JsonConvert.SerializeObject(row);
            //        var body = Encoding.UTF8.GetBytes(message);

            //        channel.BasicPublish(
            //            exchange: "",
            //            routingKey: "funcionarioQueue",
            //            body: body,
            //            basicProperties: null);

            //        contador++;

            //    }
            //}

            //if (qtdRegistros == contador)
            //    context.Scheduler.PauseJob(context.JobDetail.Key, context.CancellationToken);

            Task.CompletedTask.Wait();
        }
    }
}
