// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting.
using Quartz;
using Quartz.Spi;
using Serilog;
using StackifyLib;
using MigracaoWorkerService.JobFactory;
using MigracaoWorkerService.Service.Repository.Query.Interface;
using MigracaoWorkerService.Service.Repository.Query;
using MongoDB.Driver;
using MigracaoWorkerService.Context.Interface;
using MigracaoWorkerService.Context;
using MigracaoWorkerService.Service.Repository.Execute.Interface;
using MigracaoWorkerService.Service.Repository.Execute;
using Quartz.Impl;
using MigracaoWorkerService.Jobs;
using MigracaoWorkerService.Schedular;
using MigracaoWorkerService.JobConfiguration;
using Microsoft.AspNetCore.Identity;
using MigracaoWorkerService.Service.Models.Execute;
using System;
using Microsoft.EntityFrameworkCore;


namespace MigracaoWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine("Teste viado");

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .UseWindowsService()
               .UseSerilog()
               .ConfigureAppConfiguration((hostingContext, configBuilder) =>
               {
#if DEBUG
                   hostingContext.HostingEnvironment.EnvironmentName = "Development";
#endif

                   configBuilder.Sources.Clear();

                   var builder = new ConfigurationBuilder()
                          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                          .AddEnvironmentVariables()
                          .Build();

                   builder.ConfigureStackifyLogging();

                   configBuilder.AddConfiguration(builder);
               })

               .ConfigureServices((hostContext, services) =>
               {
                   services.AddHttpContextAccessor();
                   services.Configure<RabbitMqConfiguration>(hostContext.Configuration.GetSection("RabbitMqConfig"));
                   services.AddSingleton<IJobFactory, JobSchedulerFactory>();
                   services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

                   #region Adding JobType
                   services.AddSingleton<CidadeQueueJob>();
                   services.AddSingleton<CidadeJob>();
                   services.AddSingleton<EstadoQueueJob>();
                   services.AddSingleton<EstadoJob>();
                   services.AddSingleton<UsuarioQueueJob>();
                   services.AddSingleton<UsuarioJob>();
                   services.AddSingleton<FuncaoQueueJob>();
                   services.AddSingleton<FuncaoJob>();
                   services.AddSingleton<FuncaoUsuarioQueueJob>();
                   services.AddSingleton<FuncaoUsuarioJob>();
                   services.AddSingleton<PessoaQueueJob>();
                   services.AddSingleton<PessoaJob>();
                   services.AddSingleton<EnderecoQueueJob>();
                   services.AddSingleton<EnderecoJob>();
                   services.AddSingleton<ClienteQueueJob>();
                   services.AddSingleton<ClienteJob>();
                   services.AddSingleton<FuncionarioQueueJob>();
                   services.AddSingleton<FuncionarioJob>();
                   services.AddSingleton<FornecedorQueueJob>();
                   services.AddSingleton<FornecedorJob>();
                   


                   // services.AddSingleton<Es>
                   //services.AddSingleton<LoggerJob>();
                   #endregion

                   #region IdentityUser

                   services.AddSingleton<SQLContexto>();
                  // services.AddDbContext<SQLContexto>(opt => opt.UseSqlServer($"Server=DESKTOP-1GGA1R2\\SQLEXPRESS;Database=SGAGNT;User Id=anderson;Password=123456;"));

                   #endregion

                   // Logging Setup
                   var logPath = hostContext.Configuration.GetValue<string>("LogFilePath");
                   var logName = hostContext.Configuration.GetValue<string>("LogFileName");
                   StackifyLib.Utils.StackifyAPILogger.LogEnabled = true;

                   // Configurando o logger
                   if (hostContext.HostingEnvironment.IsDevelopment())
                   {
                       if (logPath == null)
                       {
                           logPath = Path.GetFullPath(Path.Combine(hostContext.HostingEnvironment.ContentRootPath, "..", "Log"));
                           Directory.CreateDirectory(logPath);
                       }

                       if (Directory.Exists(logPath) == false)
                       {
                           throw new DirectoryNotFoundException("Caminho especificado para a geração do log não existe.");
                       }

                       Log.Logger = new LoggerConfiguration()
                           .Enrich.FromLogContext()
                           .ReadFrom.Configuration(hostContext.Configuration)
                           .WriteTo.File(Path.Combine(logPath, logName), rollingInterval: RollingInterval.Day)
                           .WriteTo.Console()
                           .WriteTo.Stackify()
                           .CreateLogger();
                   }
                   else
                   {
                       Log.Logger = new LoggerConfiguration()
                           .Enrich.FromLogContext()
                           .ReadFrom.Configuration(hostContext.Configuration)
                           .WriteTo.Console()
                           .WriteTo.Stackify()
                           .CreateLogger();
                   }

                   Log.Information($"Aplicação iniciando com ambiente de {hostContext.HostingEnvironment.EnvironmentName}");

                   //services.AddTransient<IConnection, ConnectionFactory>();
                   services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
                   services.AddTransient(typeof(IRepositoryQueryBase<>), typeof(RepositoryQueryBase<>));
                   services.AddTransient<IMongoClient, MongoClient>();
                   services.AddTransient<IMongoDBContext, MongoDBContext>();

                   services.AddTransient<IUsuarioRepository, UsuarioRepository>();
                   services.AddTransient<IUsuarioQueryRepository, UsuarioQueryRepository>();
                   services.AddTransient<ICidadeRepository, CidadeRepository>();
                   services.AddTransient<ICidadeQueryRepository, CidadeQueryRepository>();
                   services.AddTransient<IEstadoRepository, EstadoRepository>();
                   services.AddTransient<IEstadoQueryRepository, EstadoQueryRepository>();
                   services.AddTransient<IQrtzCronExpressionRepository, QrtzCronExpressionRepository>();
                   services.AddTransient<IFuncaoRepository, FuncaoRepository>();
                   services.AddTransient<IFuncaoQueryRepository, FuncaoQueryRepository>();
                   services.AddTransient<IFuncaoUsuarioRepository, FuncaoUsuarioRepository>();
                   services.AddTransient<IFuncaoUsuarioQueryRepository, FuncaoUsuarioQueryRepository>();

                   services.AddTransient<IAgendaRepository, AgendaRepository>();
                   services.AddTransient<IAgendaQueryRepository, AgendaQueryRepository>();

                   services.AddTransient<IAgendamentoRepository, AgendamentoRepository>();
                   services.AddTransient<IAgendamentoQueryRepository, AgendamentoQueryRepository>();

                   services.AddTransient<ICargoRepository, CargoRepository>();
                   services.AddTransient<ICargoQueryRepository, CargoQueryRepository>();

                   services.AddTransient<ICargoFuncionarioRepository, CargoFuncionarioRepository>();
                   services.AddTransient<ICargoFuncionarioQueryRepository, CargoFuncionarioQueryRepository>();


                   services.AddTransient<IEnderecoRepository, EnderecoRepository>();
                   services.AddTransient<IEnderecoQueryRepository, EnderecoQueryRepository>();

                   services.AddTransient<IEmpresaRepository, EmpresaRepository>();
                   services.AddTransient<IEmpresaQueryRepository, EmpresaQueryRepository>();

                   services.AddTransient<IClienteRepository, ClienteRepository>();
                   services.AddTransient<IClienteQueryRepository, ClienteQueryRepository>();

                   services.AddTransient<IFuncionarioRepository, FuncionarioRepository>();
                   services.AddTransient<IFuncionarioQueryRepository, FuncionarioQueryRepository>();

                   services.AddTransient<IFornecedorRepository, FornecedorRepository>();
                   services.AddTransient<IFornecedorQueryRepository, FornecedorQueryRepository>();

                   services.AddTransient<IPessoaRepository, PessoaRepository>();
                   services.AddTransient<IPessoaQueryRepository, PessoaQueryRepository>();

                   services.AddTransient<IMotivoRepository, MotivoRepository>();
                   services.AddTransient<IMotivoQueryRepository, MotivoQueryRepository>();

                   services.AddTransient<IUnidadeVendaRepository, UnidadeVendaRepository>();
                   services.AddTransient<IUnidadeVendaQueryRepository, UnidadeVendaQueryRepository>();

                   services.AddTransient<IServicoRepository, ServicoRepository>();
                   services.AddTransient<IServicoQueryRepository, ServicoQueryRepository>();


                   //instalando o rabbit mq
                   //https://medium.com/geekculture/installing-rabbitmq-on-windows-4411f5114a84

                   //services.AddQuartz

                   // DI
                   // services.AddTransient<DbConnection, SqlConnection>((s) => new SqlConnection(hostContext.Configuration.GetConnectionString("FileGenerator.DB")));
                   //services.AddTransient<IJob, JobRun>();
                   // services.AddTransient<IJobFactory, JobSchedulerFactory>();
                   // services.AddTransient<IJobSchedulerService, JobSchedulerService>();
                   // services.AddTransient<IJobConfigProvider, JobConfigProvider>();

                   //// Windows Services
                   services.AddHostedService<JobSchedular>();
               });
    }
}


