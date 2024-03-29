using MigracaoWorkerService.Context;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Query
{
    [BsonCollection("ESTADO")]
    public class EstadoQuery : Event
    {
        [BsonElement(elementName: "EST_ID")]
        public int EST_ID { get; set; }

        [BsonElement(elementName: "EST_SIGLA")]
        public string EST_SIGLA { get; set; }

        [BsonElement(elementName: "EST_NOME")]
        public string EST_NOME { get; set; }

        [BsonElement(elementName: "EST_ID_REGIAO")]
        public int EST_ID_REGIAO { get; set; }
    }
}
