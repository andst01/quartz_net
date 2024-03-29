using MigracaoWorkerService.Context;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Query
{
    [BsonCollection("MOTIVO")]
    public class MotivoQuery : Event
    {
        [BsonElement("MOTI_ID")]
        public override int Id { get; set; }

        [BsonElement("MOTI_DESCRICAO")]
        public string Descricao { get; set; }

        [BsonElement("AGENDAS")]
        public virtual List<AgendaQuery> Agenda { get; set; }
    }
}
