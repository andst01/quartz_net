using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MigracaoWorkerService.JobConfiguration;
using MigracaoWorkerService.Service.Models.Execute;
using MigracaoWorkerService.Service.Repository.Execute.Interface;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using Quartz;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Jobs
{
    public class CidadeQueueJob : IJob
    {

        private readonly ICidadeRepository _repository;

        public new List<Cidade> listaCidades { get; set; }

        public int contadorGeral { get; set; } = 0;

        public int contador { get; set; } = 0;

        public int initial { get; set; } = 1;

        public int final { get; set; } = 100;

        public int qtdRegistros { get; set; } = 0;

        public int limit { get; set; } = 100;

        private readonly ILogger<CidadeJob> _logger;
        private readonly IOptions<RabbitMqConfiguration> _config;

        public CidadeQueueJob(ICidadeRepository repository, 
                              ILogger<CidadeJob> logger,
                              IOptions<RabbitMqConfiguration> config)
        {
            _repository = repository;
            _logger = logger;
            _config = config;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            //context.JobDetail.JobDataMap
            // var listaCidades = await _repository.Listar();
            //var listaCidades = new List<Cidade>();
            //var listaCidadeQueue = new List<Cidade>();
            var teste = context.JobDetail.JobDataMap.ToList();

            if (contador == 0)
                listaCidades = await _repository.ListarComParametros(initial, final);


            qtdRegistros = listaCidades.FirstOrDefault().QTD;
            //context.Scheduler.Standby(cancellationToken: default);

            var factory = new ConnectionFactory { HostName = "localhost"};
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            if(qtdRegistros > contadorGeral)
            {
                foreach (var cidade in listaCidades)
                {

                    //string QUEUE_NAME = "";
                    //QUEUE_NAME = "cidadeQueueJob";

                    channel.QueueDeclare(queue: "cidadeQueue",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

                    string message = Newtonsoft.Json.JsonConvert.SerializeObject(cidade);
                    var body = Encoding.UTF8.GetBytes(message);



                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "cidadeQueue",
                        body: body,
                        basicProperties: null


                    );

                    _logger.LogInformation($"[{nameof(CidadeQueueJob)}] contador inicial {initial} e final {final}");
                    // _logger.LogInformation($"[{nameof(CidadeQueueJob)}].[log de informações de CidadeQueue] data e hora : {DateTime.Now.ToString()}");


                    contador++;

                    if (contador == limit)
                    {
                        final = final + limit;
                        initial = initial + limit;
                        contador = 0;
                    }

                    contadorGeral++;

                }
            }
            
        }
    }
}
