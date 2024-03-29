using MigracaoWorkerService.Context;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Query
{
    [BsonCollection("PESSOA")]
    public class PessoaQuery : Event
    {
        [BsonElement("PESS_ID")]
        public override int Id { get; set; }

        [BsonElement("PESS_ESTRANGEIRO")]
        public bool EhEstrangeiro { get; set; }

        [BsonElement("PESS_OBSERVACAO")]
        public string Observacao { get; set; }

        [BsonElement("PESS_DATA_CADASTRO")]
        public DateTime DataCadastro { get; set; }

        [BsonElement("PESS_DATA_ALTERACAO")]
        public DateTime DataAlteracao { get; set; }

        [BsonElement("PESS_TELEFONE")]
        public string Telefone { get; set; }

        [BsonElement("PESS_CELULAR")]
        public string Celular { get; set; }

        [BsonElement("PESS_EMAIL")]
        public string Email { get; set; }

        [BsonElement("ENDERECO")]
        public virtual EnderecoQuery Endereco { get; set; }

        [BsonElement("PESS_ID_USUARIO")]
        public int IdUsuario { get; set; }

        [BsonElement("USUARIO")]
        public virtual UsuarioQuery Usuario { get; set; }

        [BsonElement("FUNCIONARIO")]
        public virtual FuncionarioQuery Funcionario { get; set; }

        [BsonElement("CLIENTE")]
        public virtual ClienteQuery Cliente { get; set; }

        [BsonElement("EMPRESA")]
        public virtual EmpresaQuery Empresa { get; set; }

        [BsonElement("FORNECEDOR")]
        public virtual FornecedorQuery Fornecedor { get; set; }

        [BsonElement("UNIDADEVENDA")]
        public virtual UnidadeVendaQuery UnidadeVenda { get; set; }
    }
}
