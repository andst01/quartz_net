using MigracaoWorkerService.Context;
using MigracaoWorkerService.Service.Models.Execute;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Query
{
    [BsonCollection("UNIDADE_VENDA")]
    public class UnidadeVendaQuery : Event
    {
        [BsonElement("UNVD_ID")]
        public override int Id { get; set; }

        [BsonElement("AGENDAMENTOS")]
        public List<AgendamentoQuery> Agendamentos { get; set; }

        [BsonElement("AGENDAS")]
        public List<AgendaQuery> Agendas { get; set; }

        //[BsonElement("VENDAS")]
        //public List<VendaNotification> Vendas { get; set; }

        [BsonElement("UNVD_ID_EMPRESA")]
        public int IdEmpresa { get; set; }

        [BsonElement("EMPRESA")]
        public virtual EmpresaQuery Empresa { get; set; }

        [BsonElement("PESSOA")]
        public virtual PessoaQuery Pessoa { get; set; }

        [BsonElement("UNVD_ID_PESSOA")]
        public int IdPessoa { get; set; }

        [BsonElement("UNVD_CNPJ")]
        public string CNPJ { get; set; }

        [BsonElement("UNVD_NOME_FANTASIA")]
        public string NomeFantasia { get; set; }

        [BsonElement("UNVD_RAZAO_SOCIAL")]
        public string RazaoSocial { get; set; }
    }
}
