using Amazon.Runtime.Internal.Util;
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
    public class EnderecoQueueJob : IJob
    {

        private readonly IEnderecoRepository _repository;
        private readonly ILogger<EnderecoQueueJob> _logger;

        public EnderecoQueueJob(IEnderecoRepository repository, ILogger<EnderecoQueueJob> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        private List<Endereco> lista { get; set; }

        private int qtdRegistros { get; set; } = 0;

        private int contador { get; set; } = 0;

        public async Task Execute(IJobExecutionContext context)
        {
            var retorno = _repository.ObterTodos().ToList();
            qtdRegistros = retorno.Count();

            #region Populate Endereco

            //if (qtdRegistros == 0)
            //{
            //    lista = new List<Endereco>()
            //    {
            //        new Endereco()
            //        {
            //            IdPessoa = 6,
            //            Bairro = "Ciddade da Esperança",
            //            Logradouro = "Rua A",
            //            Numero = "10",
            //            Cep = "59000000",
            //            IdCidade = 2408102

            //        },
            //        new Endereco()
            //        {
            //            IdPessoa = 7,
            //            Bairro = "Alecrim",
            //            Logradouro = "Rua C",
            //            Numero = "11",
            //            Cep = "59000000",
            //            IdCidade = 2408102

            //        },
            //        new Endereco()
            //        {
            //            IdPessoa = 8,
            //            Bairro = "Cidade Nova",
            //            Logradouro = "Rua Z",
            //            Numero = "123",
            //            Cep = "59000000",
            //            IdCidade = 2408102

            //        },
            //         new Endereco()
            //        {
            //            IdPessoa = 9,
            //            Bairro = "Candelária",
            //            Logradouro = "Rua Y",
            //            Numero = "78",
            //            Cep = "59000000",
            //            IdCidade = 2408102

            //        },
            //          new Endereco()
            //        {
            //            IdPessoa = 10,
            //            Bairro = "Barro Vermelho",
            //            Logradouro = "Rua U",
            //            Numero = "100",
            //            Cep = "59000000",
            //            IdCidade = 2408102

            //        },
            //             new Endereco()
            //        {
            //            IdPessoa = 11,
            //            Bairro = "Potengi",
            //            Logradouro = "Rua P",
            //            Numero = "54",
            //            Cep = "59000000",
            //            IdCidade = 2408102

            //        },
            //             new Endereco()
            //        {
            //            IdPessoa = 12,
            //            Bairro = "Centro",
            //            Logradouro = "Rua J",
            //            Numero = "90",
            //            Cep = "59000000",
            //            IdCidade = 2408102

            //        },
            //        new Endereco()
            //        {
            //            IdPessoa = 13,
            //            Bairro = "Cidade Alta",
            //            Logradouro = "Rua I",
            //            Numero = "84",
            //            Cep = "59000000",
            //            IdCidade = 2408102

            //        },
            //         new Endereco()
            //        {
            //            IdPessoa = 14,
            //            Bairro = "Pajuçara",
            //            Logradouro = "Rua R",
            //            Numero = "87",
            //            Cep = "59000000",
            //            IdCidade = 2408102

            //        },
            //         new Endereco()
            //        {
            //            IdPessoa = 15,
            //            Bairro = "Tirol",
            //            Logradouro = "Rua H",
            //            Numero = "94",
            //            Cep = "59000000",
            //            IdCidade = 2408102

            //        },
            //           new Endereco()
            //        {
            //            IdPessoa = 16,
            //            Bairro = "Petrópolis",
            //            Logradouro = "Rua E",
            //            Numero = "26",
            //            Cep = "59000000",
            //            IdCidade = 2408102

            //        },
            //              new Endereco()
            //        {
            //            IdPessoa = 17,
            //            Bairro = "Lagoa Nova",
            //            Logradouro = "Rua F",
            //            Numero = "26",
            //            Cep = "59000000",
            //            IdCidade = 2408102

            //        },
            //        new Endereco()
            //        {
            //            IdPessoa = 17,
            //            Bairro = "Igapó",
            //            Logradouro = "Rua G",
            //            Numero = "48",
            //            Cep = "59000000",
            //            IdCidade = 2408102

            //        }
            //    };

            //    foreach (var add in lista)
            //    {
            //        await _repository.AdicionarAsync(add);
            //    }

            //    await _repository.Save();

            //}

            #endregion

            if (qtdRegistros > contador)
            {
                foreach (var row in retorno)
                {
                    var factory = new ConnectionFactory { HostName = "localhost" };
                    using var connection = factory.CreateConnection();
                    using var channel = connection.CreateModel();

                    channel.QueueDeclare(queue: "enderecoQueue",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                    string message = Newtonsoft.Json.JsonConvert.SerializeObject(row);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "enderecoQueue",
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
