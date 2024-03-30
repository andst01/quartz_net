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
    public class PessoaQueueJob : IJob
    {
        private readonly IPessoaRepository _repository;

        public PessoaQueueJob(IPessoaRepository repository)
        {
            _repository = repository;
        }

        private List<Pessoa> lista { get;set; }

        private int qtdRegistros { get; set; } = 0;

        private int contador { get; set; } = 0;

        public async Task Execute(IJobExecutionContext context)
        {

            var retorno = _repository.ObterTodos().ToList();
            qtdRegistros = retorno.Count();

            #region Populate Pessoa
            //if (qtdRegistros == 0)
            //{
            //    lista = new List<Pessoa>()
            //    {
            //        new Pessoa()
            //        {
            //            EhEstrangeiro = false,
            //            Celular = "(84) 99999-0304",
            //            Telefone = "(84) 99999-0304",
            //            DataCadastro = DateTime.Now,
            //            Email = "ALEX@TESTE.COM.BR",
            //            IdUsuario = 5

            //        },
            //        new Pessoa()
            //        {
            //            EhEstrangeiro = false,
            //            Celular = "(84) 91265-0104",
            //            Telefone = "(84) 98542-8561",
            //            DataCadastro = DateTime.Now,
            //            Email = "JOAO@TESTE.COM.BR",
            //            IdUsuario = 6

            //        },
            //         new Pessoa()
            //        {
            //            EhEstrangeiro = false,
            //            Celular = "(84) 995627-0203",
            //            Telefone = "(84) 95628-1133",
            //            DataCadastro = DateTime.Now,
            //            Email = "RAFAEL@TESTE.COM.BR",
            //            IdUsuario = 7

            //        },
            //           new Pessoa()
            //        {
            //            EhEstrangeiro = false,
            //            Celular = "(84) 995627-0203",
            //            Telefone = "(84) 95628-1133",
            //            DataCadastro = DateTime.Now,
            //            Email = "RAFAEL@TESTE.COM.BR",
            //            IdUsuario = 7

            //        },

            //        new Pessoa()
            //        {
            //            EhEstrangeiro = false,
            //            Celular = "(84) 99875-1278",
            //            Telefone = "(84) 99875-1278",
            //            DataCadastro = DateTime.Now,
            //            Email = "ANAB@TESTE.COM.BR",
            //            IdUsuario = 8

            //        },

            //         new Pessoa()
            //        {
            //            EhEstrangeiro = false,
            //            Celular = "(84) 98451-1167",
            //            Telefone = "(84) 98451-1167",
            //            DataCadastro = DateTime.Now,
            //            Email = "MIGUEL@TESTE.COM.BR",
            //            IdUsuario = 9

            //        },
            //          new Pessoa()
            //        {
            //            EhEstrangeiro = false,
            //            Celular = "(84) 99641-1056",
            //            Telefone = "(84) 99641-1056",
            //            DataCadastro = DateTime.Now,
            //            Email = "MILTON@TESTE.COM.BR",
            //            IdUsuario = 10

            //        },

            //           new Pessoa()
            //        {
            //            EhEstrangeiro = false,
            //            Celular = "(84) 98741-1569",
            //            Telefone = "(84) 98741-1569",
            //            DataCadastro = DateTime.Now,
            //            Email = "JOSE@TESTE.COM.BR",
            //            IdUsuario = 11

            //        },
            //            new Pessoa()
            //        {
            //            EhEstrangeiro = false,
            //            Celular = "(84) 98613-1248",
            //            Telefone = "(84) 98613-1248",
            //            DataCadastro = DateTime.Now,
            //            Email = "MARIA@TESTE.COM.BR",
            //            IdUsuario = 12

            //        },
            //             new Pessoa()
            //        {
            //            EhEstrangeiro = false,
            //            Celular = "(84) 90672-1322",
            //            Telefone = "(84) 90672-1322",
            //            DataCadastro = DateTime.Now,
            //            Email = "ANDERSON@TESTE.COM.BR",
            //            IdUsuario = 13

            //        },

            //             new Pessoa()
            //        {
            //            EhEstrangeiro = false,
            //            Celular = "(84) 98723-1167",
            //            Telefone = "(84) 98723-1167",
            //            DataCadastro = DateTime.Now,
            //            Email = "PHSA@TESTE.COM.BR",
            //            IdUsuario = 14

            //        },
            //             new Pessoa()
            //        {
            //            EhEstrangeiro = false,
            //            Celular = "(84) 99542-1095",
            //            Telefone = "(84) 99542-1095",
            //            DataCadastro = DateTime.Now,
            //            Email = "PHSAFF1@TESTE.COM.BR",
            //            IdUsuario = 15

            //        },
            //             new Pessoa()
            //        {
            //            EhEstrangeiro = false,
            //            Celular = "(84) 99542-1094",
            //            Telefone = "(84) 99542-1094",
            //            DataCadastro = DateTime.Now,
            //            Email = "PHSAFF2@TESTE.COM.BR",
            //            IdUsuario = 16

            //        },
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

                    channel.QueueDeclare(queue: "pessoaQueue",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                    string message = Newtonsoft.Json.JsonConvert.SerializeObject(row);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "pessoaQueue",
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
