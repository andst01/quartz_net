using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MigracaoWorkerService.JobConfiguration;
using MigracaoWorkerService.Service.Models.Execute;
using MigracaoWorkerService.Service.Models.Query;
using MigracaoWorkerService.Service.Repository.Execute.Interface;
using MigracaoWorkerService.Service.Repository.Query.Interface;
using Newtonsoft.Json;
using Quartz;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MigracaoWorkerService.Jobs
{

    public class CidadeJob : IJob
    {
        private readonly ILogger<CidadeJob> _logger;

        //private readonly List<Cidade> listCidades = new List<Cidade>();
        private readonly ICidadeRepository _repository;
        private readonly ICidadeQueryRepository _repositoryQuery;
        private readonly RabbitMqConfiguration _config;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IMapper mapper;


        public CidadeJob(ILogger<CidadeJob> logger,
                        ICidadeRepository repository,
                        ICidadeQueryRepository repositoryQuery,
                        IOptions<RabbitMqConfiguration> config)
        {
            _repository = repository;
            _repositoryQuery = repositoryQuery;
            _logger = logger;
            _config = config.Value;


            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                        queue: "cidadeQueue",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);



        }

        public Task Execute(IJobExecutionContext context)
        {


            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);
                var message = JsonConvert.DeserializeObject<CidadeQuery>(contentString);

                // _usuarioRepository.ObterTodos();
                //_repositoryQuery.Add(message);

                _channel.BasicAck(eventArgs.DeliveryTag, false);

                //var teste = JsonConvert.SerializeObject(message);
                _logger.LogInformation($"[{nameof(CidadeJob)}].[log de informações de Cidades] response : {JsonConvert.SerializeObject(message)}");
                _logger.LogInformation($"[{nameof(CidadeJob)}].[log de informações de Cidades] contadores data hora : {DateTime.Now.ToString()}");


            };

            _channel.BasicConsume("cidadeQueue", false, consumer);

            return Task.CompletedTask;
        }

       



    }
}
