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
    [BsonCollection("FUNCIONARIO")]
    public class FuncionarioQuery : Event
    {
        [BsonElement("FNCR_ID")]
        public override int Id { get; set; }

        [BsonElement("RESPONSAVEIS_SERVICOS")]
        public List<AgendamentoQuery> ResponsaveisServico { get; set; }

        [BsonElement("ATENDENTES")]
        public List<AgendamentoQuery> Atendentes { get; set; }

        [BsonElement("CARGOS_FUNCIONARIOS")]
        public List<CargoFuncionarioQuery> CargosFuncionario { get; set; }

        [BsonElement("AGENDAS")]
        public List<AgendaQuery> Agendas { get; set; }

        [BsonElement("FNCR_ID_PESSOA")]
        public int IdPessoa { get; set; }

        [BsonElement("PESSOA")]
        public virtual PessoaQuery Pessoa { get; set; }

        [BsonElement("FNCR_CPF")]
        public string CPF { get; set; }

        [BsonElement("FNCR_RG")]
        public string RG { get; set; }

        [BsonElement("FNCR_NOME")]
        public string Nome { get; set; }

        [BsonElement("FNCR_DATA_NASCIMENTO")]
        public DateTime DataNascimento { get; set; }

        //[BsonElement("VENDAS")]
        //public List<Venda> Vendas { get; set; }
    }
}
