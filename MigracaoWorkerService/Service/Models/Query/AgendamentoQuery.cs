using MigracaoWorkerService.Context;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Query
{
    [BsonCollection("AGENDAMENTO")]
    public class AgendamentoQuery : Event
    {
        [BsonElement(elementName: "AGMT_ID")]
        public override int Id { get; set; }

        [BsonElement(elementName: "AGMT_DATA_INICIO")]
        public DateTime DataInicio { get; set; }

        [BsonElement(elementName: "AGMT_DATA_FINAL")]
        public DateTime DataFinal { get; set; }

        [BsonElement(elementName: "AGMT_DIA_INTEIRO")]
        public bool DiaInteiro { get; set; }

        [BsonElement(elementName: "AGMT_VISITA_EM_CASA")]
        public bool VisitaEmCasa { get; set; }

        [BsonElement(elementName: "AGMT_STATUS")]
        public int Status { get; set; }

        [BsonElement(elementName: "RESPONSAVEL_SERVICO")]
        public virtual FuncionarioQuery ResponsavelServico { get; set; }

        [BsonElement(elementName: "AGMT_ID_RESP_SERVICO")]
        public int? IdResponsavelServico { get; set; }

        [BsonElement(elementName: "AGMT_ID_ATENDENTE")]
        public int? IdAtendente { get; set; }

        [BsonElement(elementName: "ATENDENTE")]
        public virtual FuncionarioQuery Atendente { get; set; }

        [BsonElement(elementName: "CLIENTE")]
        public virtual ClienteQuery Cliente { get; set; }

        [BsonElement(elementName: "AGMT_ID_CLIENTE")]
        public int IdCliente { get; set; }

        [BsonElement(elementName: "SERVICOS")]
        public virtual List<ServicoQuery> Servico { get; set; }

        [BsonElement(elementName: "AGMT_ID_UNIDADEVENDA")]
        public int? IdUnidadeVenda { get; set; }

        [BsonElement(elementName: "UNIDADEVENDA")]
        public virtual UnidadeVendaQuery UnidadeVenda { get; set; }
    }
}
