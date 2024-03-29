using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MigracaoWorkerService.Service.Models.Execute;
using MigracaoWorkerService.Service.Repository.Execute.Interface;
using Quartz;
using RabbitMQ.Client;
using System.Text;

namespace MigracaoWorkerService.Jobs
{
    public class EstadoQueueJob : IJob
    {
       // private readonly ILogger<CidadeJob> _logger;
        private readonly ILogger<EstadoQueueJob> _logger;
        private readonly IEstadoRepository _repository;

        public new List<Estado> listaEstados { get; set; }

       // private readonly UserManager<IdentityUser> _userManager;

        public int contadorGeral { get; set; } = 0;

        //public int contador { get; set; } = 0;

        //public int initial { get; set; } = 1;

        //public int final { get; set; } = 100;

        public int qtdRegistros { get; set; } = 0;

        //public int limit { get; set; } = 100;

        public EstadoQueueJob(IEstadoRepository repository, 
                              ILogger<EstadoQueueJob> logger)
        {
            _repository = repository;
            _logger = logger;
           // _userManager = userManager;
            //userManager.CreateAsync = new UserManager<Usuario>()
        }


        public async Task Execute(IJobExecutionContext context)
        {

            //if (contador == 0)

            listaEstados = await _repository.Listar();

            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            qtdRegistros = listaEstados.Max(x => x.ROW_ID);
            //var userManager = new UserManager<ApplicationUser>();
            //_userManager.CreateAsync(new )
           // UserManager.

           
            if (qtdRegistros > contadorGeral)
            {
                foreach (var estado in listaEstados)
                {
                    channel.QueueDeclare(queue: "estadoQueue",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

                    string message = Newtonsoft.Json.JsonConvert.SerializeObject(estado);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "estadoQueue",
                        body: body,
                        basicProperties: null);

                    qtdRegistros = estado.ROW_ID;
                    contadorGeral++;

                    _logger.LogInformation($"[{nameof(EstadoQueueJob)}] quantidade: {qtdRegistros}");

                }
            }


            //throw new NotImplementedException();
        }
    }
}
