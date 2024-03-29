using MigracaoWorkerService.Context;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Query
{
    [BsonCollection("EMPRESA")]
    public class EmpresaQuery : Event
    {
        [BsonElement(elementName: "EMPR_ID")]
        public override int Id { get; set; }

        [BsonElement(elementName: "EMPR_ID_PESSOA")]
        public int IdPessoa { get; set; }

        [BsonElement(elementName: "EMPR_CNPJ")]
        public string CNPJ { get; set; }

        [BsonElement(elementName: "EMPR_NOME_FANTASIA")]
        public string NomeFantasia { get; set; }

        [BsonElement(elementName: "UNIDADEVENDAS")]
        public List<UnidadeVendaQuery> UnidadeVendas { get; set; }

        [BsonElement(elementName: "PESSOA")]
        public PessoaQuery Pessoa { get; set; }

        [BsonElement(elementName: "EMPR_RAZAO_SOCIAL")]
        public string RazaoSocial { get; set; }
    }
}
