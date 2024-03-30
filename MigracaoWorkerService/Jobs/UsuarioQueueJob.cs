using Microsoft.EntityFrameworkCore;
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
    public class UsuarioQueueJob : IJob
    {
        private readonly ILogger<UsuarioQueueJob> _logger;
        private readonly IUsuarioRepository _repository;

        public UsuarioQueueJob(ILogger<UsuarioQueueJob> logger, 
                               IUsuarioRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public new List<Usuario> listaUsuario { get; set; }

        public int contadorGeral { get; set; } = 0;

        public int qtdRegistros { get; set; } = 0;

        public Task Execute(IJobExecutionContext context)
        {
            var retorno = new List<Usuario>();

            List<int> ids =  new List<int>() { 5, 6, 7 };

            if(qtdRegistros == 0)
                 retorno = _repository.ObterTodos().Where(x => !ids.Contains(x.Id)).ToList();

            qtdRegistros = retorno.Count();

            if (qtdRegistros > contadorGeral)
            {

                var factory = new ConnectionFactory { HostName = "localhost" };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                foreach (var row in retorno)
                {
                    channel.QueueDeclare(queue: "usuarioQueue",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                    string message = Newtonsoft.Json.JsonConvert.SerializeObject(row);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "usuarioQueue",
                        body: body,
                        basicProperties: null);

                    contadorGeral++;
                }
            }

           

            return Task.CompletedTask;
            //throw new NotImplementedException();
        }
    }
}
