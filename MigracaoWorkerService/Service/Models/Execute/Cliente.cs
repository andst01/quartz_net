using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Execute
{
    public class Cliente
    {
        public int Id { get; set; }

       // public List<Agendamento> Agendamentos { get; set; }

       // public List<Venda> Vendas { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public int IdPessoa { get; set; }

        public string CPF { get; set; }

        public string RG { get; set; }

        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }
    }
}
