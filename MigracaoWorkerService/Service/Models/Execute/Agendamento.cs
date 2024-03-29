using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Execute
{
    public class Agendamento
    {
        public int Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFinal { get; set; }
        public bool DiaInteiro { get; set; }
        public bool VisitaEmCasa { get; set; }
        public int Status { get; set; }

        public virtual Funcionario ResponsavelServico { get; set; }

        public int IdResponsavelServico { get; set; }

        public int IdAtendente { get; set; }

        public virtual Funcionario Atendente { get; set; }

        public virtual Cliente Cliente { get; set; }
        public int IdCliente { get; set; }

        public virtual ICollection<Servico> Servico { get; set; }

        public int? IdUnidadeVenda { get; set; }
        public virtual UnidadeVenda UnidadeVenda { get; set; }
    }
}
