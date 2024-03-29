using MigracaoWorkerService.Context;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Query
{

    [BsonCollection("AGENDA")]
    public class AgendaQuery : Event
    {
        [BsonElement("AGDA_ID")]
        public override int Id { get; set; }
        [BsonElement(elementName: "AGDA_DATA_INICIO")]
        public DateTime DataInicio { get; set; }
        [BsonElement(elementName: "AGDA_DATA_FIM")]
        public DateTime DataFim { get; set; }
        [BsonElement(elementName: "AGDA_ID_UNIDADEVENDA")]
        public int IdUnidadeVenda { get; set; }
        [BsonElement(elementName: "AGDA_PRESENCA")]
        public int Presenca { get; set; }
        [BsonElement(elementName: "AGDA_ID_MOTIVO")]
        public int IdMotivo { get; set; }
        [BsonElement(elementName: "MOTIVO")]
        public virtual MotivoQuery Motivo { get; set; }
        [BsonElement(elementName: "AGDA_OBSERVACAO")]
        public string Observacao { get; set; }
        [BsonElement(elementName: "UNIDADE_VENDA")]
        public UnidadeVendaQuery UnidadeVenda { get; set; }

        [BsonElement(elementName: "FUNCIONARIO")]
        public FuncionarioQuery Funcionario { get; set; }

        [BsonElement(elementName: "AGDA_ID_FUNCIONARIO")]
        public int IdFuncionario { get; set; }
    }
}
