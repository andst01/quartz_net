using MigracaoWorkerService.Context;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Query
{
    [BsonCollection("SERVICO")]
    public class ServicoQuery : Event
    {
        [BsonElement("SERV_ID")]
        public override int Id { get; set; }

        [BsonElement("SERV_ID_AGENDAMENTO")]
        public int IdAgendamento { get; set; }

        [BsonElement("AGENDAMENTO")]
        public virtual AgendamentoQuery Agendamento { get; set; }

        [BsonElement("SERV_ID_RESPONSAVEL")]
        public int IdUsuarioResponsavel { get; set; }

        [BsonElement("SERV_COD_SERVICO")]
        public int CodigoServico { get; set; }

        [BsonElement("SERV_DATA_CADASTRO")]
        public DateTime DataCadastro { get; set; }

        [BsonElement("SERV_ID_USUARIO")]
        public int IdUsuarioCriacao { get; set; }

        [BsonElement("SERV_QTD_PARCELAS")]
        public int QuantidadeParcelas { get; set; }

        [BsonElement("SERV_VL_TOTAL")]
        public decimal ValorTotal { get; set; }

        [BsonElement("USUARIO_CRIACAO")]
        public UsuarioQuery UsuarioCriacao { get; set; }

        [BsonElement("USUARIO_RESPONSAVEL")]
        public UsuarioQuery UsuarioResponsavel { get; set; }
    }
}
