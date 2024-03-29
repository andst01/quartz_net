using Microsoft.Extensions.Logging;
using MigracaoWorkerService.JobConfiguration.Contracts;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.JobConfiguration.Modelos
{
    public class JobRun : IJob
    {
        private readonly ILogger<JobRun> _logger;

        // Guarda a referencia ao provedor de dados de configuração da janela
        private readonly IJobConfigProvider _jobConfigProvider;

        /// <summary>
		/// Construtor do executor de tarefas
		/// </summary>
		public JobRun(ILogger<JobRun> logger, IJobConfigProvider jobConfigProvider)
        {
            this._logger = logger;
            this._jobConfigProvider = jobConfigProvider;
        }

        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }

        private async Task RegistrarJanelaProcessamento(string jobName, DateTime dataInicial, DateTime dataFinal, string message, Dictionary<string, KeyValuePair<string, string>> dados)
        {
            try
            {
                this._logger.LogDebug($"O Job {jobName} começou as {dataInicial} e terminou as {dataFinal}: {message}");

                await this._jobConfigProvider.LogExecution(jobName, dataInicial, dataFinal, message, dados);
            }
            catch (Exception e) // Não pode dar erro no Finally
            {
                var msg = $"[{this.GetType().Name}.{nameof(RegistrarJanelaProcessamento)}:{jobName}]: Aconteceu um erro imprevisto";
                this._logger.LogError(e, msg);
            }
        }


  //      /// <summary>
		///// Obtem a fabrica responsavel por construir os itens de processo da janela
		///// </summary>
		//private IFactory GetFactory(IJobConfig jobConfig)
  //      {
  //          var basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
  //          if (!basePath.EndsWith(Path.DirectorySeparatorChar))
  //              basePath += Path.DirectorySeparatorChar.ToString();
  //          return Activator.CreateInstance(Assembly.LoadFile(basePath + jobConfig.Assembly).GetType(jobConfig.Factory)) as IFactory;
  //      }
    }
}
