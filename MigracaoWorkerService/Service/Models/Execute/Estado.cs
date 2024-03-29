using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Execute
{
    public class Estado
    {
        public int EST_ID { get; set; }

        public string EST_SIGLA { get; set; }

        public string EST_NOME { get; set; }

        public int EST_ID_REGIAO { get; set; }

        public int ROW_ID { get; set; }


    }
}
