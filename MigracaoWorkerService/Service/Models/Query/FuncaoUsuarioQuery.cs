using MigracaoWorkerService.Context;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Query
{
    [BsonCollection("FUNCAO_USUARIO")]
    public class FuncaoUsuarioQuery : Event
    {
        [BsonElement("FNUS_ID_USUARIO")]
        public int UserId { get; set; }

        [BsonElement("FNUS_ID_FUNCAO")]
        public int RoleId { get; set; }

        //[BsonElement("FUNCAO")]
        //public FuncaoNotification Role { get; set; }

        //[BsonElement("USUARIO")]
        //public UsuarioNotification User { get; set; }

        [BsonElement("FUNC_DT_INICIO")]
        public DateTime DataInicio { get; set; }

        [BsonElement("FUNC_DT_FIM")]
        public DateTime? DataFim { get; set; }
    }
}
