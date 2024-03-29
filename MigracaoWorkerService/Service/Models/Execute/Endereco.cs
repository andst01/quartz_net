using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Execute
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }

        public int IdCidade { get; set; }

        public virtual Cidade Cidade { get; set; }

        //[ForeignKey(nameof(IdPessoa))]
        //[InverseProperty("Endereco")]
        public virtual Pessoa Pessoa { get; set; }

        public int IdPessoa { get; set; }
    }
}
