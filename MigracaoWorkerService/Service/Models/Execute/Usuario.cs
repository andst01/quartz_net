using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Execute
{
    public class Usuario : IdentityUser<int>
    {
        public override int Id { get; set; }
        public string Password { get; set; }
        public string Genero { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Nome { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }

        //[NotMapped]
        public virtual Pessoa Pessoa { get; set; }

        public override DateTimeOffset? LockoutEnd { get; set; }

        public virtual ICollection<Servico> ServicoCriacao { get; set; }

        public virtual ICollection<Servico> ServicoResponsavel { get; set; }

        [NotMapped]
        public virtual ICollection<FuncaoUsuario> FuncoesUsuarios { get; set; }
    }
}
