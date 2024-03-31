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
    public class CargoFuncionarioQueueJob : IJob
    {
        private readonly ICargoFuncionarioRepository _repository;
        private readonly ILogger<CargoFuncionarioQueueJob> _logger;

        private List<CargoFuncionario> lista { get; set; }

        private int qtdRegistros { get; set; } = 0;

        private int contador { get; set; } = 0;

        public CargoFuncionarioQueueJob(ICargoFuncionarioRepository repository, 
                                        ILogger<CargoFuncionarioQueueJob> logger)
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
            //    lista = new List<CargoFuncionario>()
            //    {
            //        new CargoFuncionario()
            //        {
            //            IdFuncionario = 12,
            //            IdCargo = 69,
            //            DataFim = DateTime.Parse("20/03/2021", CultureInfo.GetCultureInfo("pt-br")),
            //            DataInicio =  DateTime.Parse("15/02/2019", CultureInfo.GetCultureInfo("pt-br")),
            //        },
            //         new CargoFuncionario()
            //        {
            //            IdFuncionario = 12,
            //            IdCargo = 70,
            //            DataFim = DateTime.Parse("01/06/2022", CultureInfo.GetCultureInfo("pt-br")),
            //            DataInicio =  DateTime.Parse("21/03/2021", CultureInfo.GetCultureInfo("pt-br")),
                        
            //        },
            //          new CargoFuncionario()
            //        {
            //            IdFuncionario = 12,
            //            IdCargo = 71,
            //            // DataFim = DateTime.Parse("01/06/2022", CultureInfo.GetCultureInfo("pt-br")),
            //            DataInicio =  DateTime.Parse("02/06/2022", CultureInfo.GetCultureInfo("pt-br")),
            //        },
            //         new CargoFuncionario()
            //        {
            //            IdFuncionario = 13,
            //            IdCargo = 68,
            //            DataFim = DateTime.Parse("30/04/2020", CultureInfo.GetCultureInfo("pt-br")),
            //            DataInicio =  DateTime.Parse("01/02/2018", CultureInfo.GetCultureInfo("pt-br")),
            //        },
            //          new CargoFuncionario()
            //        {
            //            IdFuncionario = 13,
            //            IdCargo = 67,
            //           // DataFim = DateTime.Parse("20/03/2021", CultureInfo.GetCultureInfo("pt-br")),
            //            DataInicio =  DateTime.Parse("02/05/2020", CultureInfo.GetCultureInfo("pt-br")),
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

                    channel.QueueDeclare(queue: "cargoFuncionarioQueue",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                    string message = Newtonsoft.Json.JsonConvert.SerializeObject(row);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "cargoFuncionarioQueue",
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
