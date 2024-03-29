using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Execute
{
    public class Cidade
    {
        public int CIDA_ID { get; set; }

        public string CIDA_NOME { get; set; }

        public int CIDA_ID_ESTADO { get; set; }

        public int CIDA_ID_MICROREGIAO { get; set; }

        public int ROW_ID { get; set; }

        public int QTD { get; set; }
    }

    public class ResultCidade
    {
        public ResultCidade()
        {
            listCidades = new List<Cidade>();    
        }
        public List<Cidade> listCidades { get; set; }
    }
}
