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
    [BsonCollection("CLIENTE")]
    public class ClienteQuery : Event
    {
        [BsonElement(elementName: "CLIE_ID")]
        public override int Id { get; set; }

        [BsonElement(elementName: "AGENDAMENTOS")]
        public List<AgendamentoQuery> Agendamentos { get; set; }

        //[BsonElement(elementName: "VENDAS")]
        //public List<Venda> Vendas { get; set; }

        [BsonElement(elementName: "PESSOA")]
        public virtual PessoaQuery Pessoa { get; set; }

        [BsonElement(elementName: "CLIE_ID_PESSOA")]
        public int IdPessoa { get; set; }

        [BsonElement(elementName: "CLIE_CPF")]
        public string CPF { get; set; }

        [BsonElement(elementName: "CLIE_RG")]
        public string RG { get; set; }

        [BsonElement(elementName: "CLIE_NOME")]
        public string Nome { get; set; }

        [BsonElement(elementName: "CLIE_DATA_NASCIMENTO")]
        public DateTime DataNascimento { get; set; }
    }
}
