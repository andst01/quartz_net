using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Query
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public ObjectId MongoId { get; set; }

        public virtual int Id { get; set; }
        public Event()
        {
            MongoId = ObjectId.GenerateNewId();
        }
        //public virtual int Id { get; set; }
    }
}
