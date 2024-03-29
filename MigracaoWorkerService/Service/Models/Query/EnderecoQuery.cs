using MigracaoWorkerService.Context;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Query
{
    [BsonCollection("ENDERECO")]
    public class EnderecoQuery : Event
    {
        [BsonElement(elementName: "ENDE_ID")]
        public override int Id { get; set; }

        [BsonElement(elementName: "ENDE_LOGRADOURO")]
        public string Logradouro { get; set; }

        [BsonElement(elementName: "ENDE_NUMERO")]
        public string Numero { get; set; }

        [BsonElement(elementName: "ENDE_BAIRRO")]
        public string Bairro { get; set; }

        [BsonElement(elementName: "ENDE_COMPLEMENTO")]
        public string Complemento { get; set; }

        [BsonElement(elementName: "ENDE_CEP")]
        public string Cep { get; set; }

        [BsonElement(elementName: "ENDE_ID_CIDADE")]
        public int IdCidade { get; set; }

        [BsonElement(elementName: "CIDADE")]
        public virtual CidadeQuery Cidade { get; set; }

        [BsonElement(elementName: "PESSOA")]
        public virtual PessoaQuery Pessoa { get; set; }

        [BsonElement(elementName: "ENDE_ID_PESSOA")]
        public int IdPessoa { get; set; }
    }
}
