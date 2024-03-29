using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Execute
{
    public class UnidadeVenda
    {
        public int Id { get; set; }

        public virtual ICollection<Agendamento> Agendamentos { get; set; }

        public virtual ICollection<Agenda> Agendas { get; set; }

        //public virtual ICollection<Venda> Vendas { get; set; }

        public int IdEmpresa { get; set; }

        public virtual Empresa Empresa { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public int IdPessoa { get; set; }

        public string CNPJ { get; set; }

        public string NomeFantasia { get; set; }

        public string RazaoSocial { get; set; }
    }
}
