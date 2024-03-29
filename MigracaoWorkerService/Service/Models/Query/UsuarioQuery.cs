using MigracaoWorkerService.Context;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Query
{
    [BsonCollection("USUARIO")]
    public class UsuarioQuery : Event
    {
        [BsonElement("USUA_ID")]
        public override int Id { get; set; }

        [BsonElement("USUA_GENERO")]
        public string Genero { get; set; }

        [BsonElement("USUA_DATA_NASCIMENTO")]
        public DateTime DataNascimento { get; set; }

        [BsonElement("USUA_NOME")]
        public string Nome { get; set; }

        //[BsonElement("FUNCOES_USUARIOS")]
        //public ICollection<FuncaoUsuarioNotification> FuncoesUsuarios { get; set; }

        [BsonElement("USUA_DATA_CRIACAO")]
        public DateTime DataCriacao { get; set; }

        [BsonElement("USUA_DATA_ALTERACAO")]
        public DateTime DataAlteracao { get; set; }

        //[BsonElement("PESSOA")]
        //public virtual PessoaNotification Pessoa { get; set; }

        //[BsonElement("SERVICOS_CRIACOES")]
        //public List<ServicoNotification> ServicoCriacao { get; set; }

        //[BsonElement("SERVICOS_RESPONSAVEIS")]
        //public List<ServicoNotification> ServicoResponsavel { get; set; }

        [BsonElement("USUA_LOCKOUT_END")]
        public virtual DateTimeOffset? LockoutEnd { get; set; }

        [BsonElement("USUA_TWOFACTOR_ENABELD")]
        public virtual bool TwoFactorEnabled { get; set; }

        [BsonElement("USUA_PHONENUMBER_CONFIRMED")]
        public virtual bool PhoneNumberConfirmed { get; set; }

        [BsonElement("USUA_PHONUMBER")]
        public virtual string PhoneNumber { get; set; }

        [BsonElement("USUA_CONCURRENCY_TEMP")]
        public virtual string ConcurrencyStamp { get; set; }

        [BsonElement("USUA_SECURITY_TEMP")]
        public virtual string SecurityStamp { get; set; }

        [BsonElement("USUA_PASSWORD_HASH")]
        public virtual string PasswordHash { get; set; }

        [BsonElement("USUA_EMAIL_CONFIRMED")]
        public virtual bool EmailConfirmed { get; set; }

        [BsonElement("USUA_NORMALIZED_EMAIL")]
        public virtual string NormalizedEmail { get; set; }

        [BsonElement("USUA_EMAIL")]
        public virtual string Email { get; set; }

        [BsonElement("USUA_NORMALIZED_NAME")]
        public virtual string NormalizedUserName { get; set; }

        [BsonElement("USUA_USERNAME")]
        public virtual string UserName { get; set; }

        [BsonElement("USUA_LOCKOUT_ENABLED")]
        public virtual bool LockoutEnabled { get; set; }

        [BsonElement("USUA_ACCESS_FAILED_COUNT")]
        public virtual int AccessFailedCount { get; set; }

    }
}
