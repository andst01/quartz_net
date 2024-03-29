using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Execute
{
    public class Funcionario
    {

        public int Id { get; set; }

        public int IdPessoa { get; set; }

        //[ForeignKey(nameof(IdPessoa))]
        //[InverseProperty("Funcionario")]
        public virtual Pessoa Pessoa { get; set; }

        public string CPF { get; set; }

        public string RG { get; set; }

        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public virtual ICollection<CargoFuncionario> CargosFuncionario { get; set; }

        public virtual ICollection<Agendamento> ResponsaveisServico { get; set; }
        public virtual ICollection<Agendamento> Atendentes { get; set; }

        //public virtual ICollection<Venda> Vendas { get; set; }

        public virtual ICollection<Agenda> Agendas { get; set; }
    }
}
