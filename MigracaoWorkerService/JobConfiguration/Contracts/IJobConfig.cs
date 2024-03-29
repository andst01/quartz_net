using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.JobConfiguration.Contracts
{
    public interface IJobConfig
    {
        /// <summary>
        /// Obtem ou define o nome do job
        /// </summary>
        string Nome { get; set; }

        /// <summary>
        /// Obtem o nome do trigger da configuração.
        /// </summary>
        string TiggerName { get; }

        /// <summary>
        /// Obtem ou define a descrição da configuração do job
        /// </summary>
        string Descricao { get; set; }

        /// <summary>
        /// Obtem ou define a coleção de triggers que vão disparar o job
        /// </summary>
        IList<IJobStarter> JobStarters { get; set; }

        /// <summary>
        /// Obtem ou define os paths do job
        /// </summary>
        IList<IJobPath> Paths { get; set; }

        /// <summary>
        /// Obtem ou define a data/hora de criação do Job
        /// </summary>
        DateTime Criacao { get; set; }

        /// <summary>
        /// Obtem ou define a data/hora da ultima modificação
        /// </summary>
        DateTime Modificacao { get; set; }

        /// <summary>
        /// Obtem ou define a DLL que possui o objeto que será instanciado
        /// </summary>
        string Assembly { get; set; }

        /// <summary>
        /// Obtem ou define a fabrica responsavel por instanciar o objeto usado pela tarefa
        /// </summary>
        string Factory { get; set; }

        /// <summary>
        /// Obtem ou define os parametros de configuração do job
        /// </summary>
        IDictionary<string, string> Parameters { get; set; }
    }
}
