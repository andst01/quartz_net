using MigracaoWorkerService.JobConfiguration.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.JobConfiguration.Modelos
{
    public class JobConfig : IJobConfig
    {
        /// <summary>
        /// Obtem ou define a identificação do Job
        /// </summary>
        public int JobConfigId { get; set; }

        /// <summary>
        /// Obtem ou define o nome do job
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Obtem o nome do trigger da configuração.
        /// </summary>
        public string TiggerName { get { return $"TGR_{this.Nome}_{this.Modificacao.ToString("yyyyMMddHHmmss")}"; } }

        /// <summary>
        /// Obtem ou define a descrição da configuração do job
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Obtem ou define a coleção de triggers que vão disparar o job
        /// </summary>
        public IList<IJobStarter> JobStarters { get; set; }

        /// <summary>
        /// Obtem ou define os paths do job
        /// </summary>
        public IList<IJobPath> Paths { get; set; }

        /// <summary>
        /// Obtem ou define a data/hora de criação do Job
        /// </summary>
        public DateTime Criacao { get; set; }

        /// <summary>
        /// Obtem ou define a data/hora da ultima modificação
        /// </summary>
        public DateTime Modificacao { get; set; }

        /// <summary>
        /// Obtem ou define a DLL que possui o objeto que será instanciado
        /// </summary>
        public string Assembly { get; set; }

        /// <summary>
        /// Obtem ou define a fabrica responsavel por instanciar o objeto usado pela tarefa
        /// </summary>
        public string Factory { get; set; }

        /// <summary>
        /// Obtem ou define os parametros de configuração do job
        /// </summary>
        public IDictionary<string, string> Parameters { get; set; }
    }
}
