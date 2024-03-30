using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Execute
{
    public class Pessoa
    {
        public int Id { get; set; }
        public bool? EhEstrangeiro { get; set; }

        public string Observacao { get; set; }

        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }

        public virtual Endereco Endereco { get; set; }

        public int IdUsuario { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Funcionario Funcionario { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual Empresa Empresa { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }

        public virtual UnidadeVenda UnidadeVenda { get; set; }
    }
}
