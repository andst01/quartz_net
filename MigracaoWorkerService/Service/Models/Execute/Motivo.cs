using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Execute
{
    public class Motivo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public virtual List<Agenda> Agenda { get; set; }
    }
}
