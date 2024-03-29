using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Execute
{
    public class Agenda
    {
        public int Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public int? IdUnidadeVenda { get; set; }
        public int Presenca { get; set; }
        public int IdMotivo { get; set; }
        public virtual Motivo Motivo { get; set; }
        public string Observacao { get; set; }

        public virtual UnidadeVenda UnidadeVenda { get; set; }

        public Funcionario Funcionario { get; set; }

        public int IdFuncionario { get; set; }

    }
}
