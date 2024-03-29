using MigracaoWorkerService.Context;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Query
{
    [BsonCollection("CIDADE")]
    public class CidadeQuery : Event
    {
        [BsonElement(elementName: "CIDA_ID")]
        public int CIDA_ID { get; set; }

        [BsonElement(elementName: "CIDA_NOME")]
        public string CIDA_NOME { get; set; }

        [BsonElement(elementName: "CIDA_ID_ESTADO")]
        public int CIDA_ID_ESTADO { get; set; }

        [BsonElement(elementName: "CIDA_ID_MICROREGIAO")]
        public int CIDA_ID_MICROREGIAO { get; set; }

        [BsonIgnore]
        public int ROW_ID { get; set; }
    }
}
