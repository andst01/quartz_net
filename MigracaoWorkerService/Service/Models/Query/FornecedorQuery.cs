using MigracaoWorkerService.Context;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Query
{
    [BsonCollection("FORNECEDOR")]
    public class FornecedorQuery : Event
    {
        [BsonElement("FORN_ID")]
        public override int Id { get; set; }

        [BsonElement("FORN_ID_PESSOA")]
        public int IdPessoa { get; set; }

        [BsonElement("FORN_CNPJ")]
        public string CNPJ { get; set; }

        [BsonElement("FORN_NOME_FANTASIA")]
        public string NomeFantasia { get; set; }

        [BsonElement("FORN_RAZAO_SOCIAL")]
        public string RazaoSocial { get; set; }

        [BsonElement("PESSOA")]
        public PessoaQuery Pessoa { get; set; }

    }
}
