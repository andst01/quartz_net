using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Execute
{
    public class Cargo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public int Ativo { get; set; }

        public virtual ICollection<CargoFuncionario> CargosFuncionario { get; set; }
    }
}
