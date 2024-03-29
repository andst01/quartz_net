using MigracaoWorkerService.Context;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Query
{

    [BsonCollection("FUNCAO")]
    public class FuncaoQuery : Event
    {
        [BsonElement("FUNC_ID")]
        public override int Id { get; set; }

        [BsonElement("FUNC_NOME")]
        public virtual string Name { get; set; }

        [BsonElement("FUNC_NORMALIZED_NAME")]
        public virtual string NormalizedName { get; set; }

        [BsonElement("FUNC_CONCURRENCY_TEMP")]
        public virtual string ConcurrencyStamp { get; set; }

        
    }
}
