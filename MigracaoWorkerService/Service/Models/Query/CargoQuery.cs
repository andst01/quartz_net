using MigracaoWorkerService.Context;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Query
{
    [BsonCollection("CARGO")]
    public class CargoQuery : Event
    {
        [BsonElement(elementName: "CARG_ID")]
        public override int Id { get; set; }

        [BsonElement(elementName: "CARG_DESCRICAO")]
        public string Descricao { get; set; }

        [BsonElement(elementName: "CARG_ATIVO")]
        public int Ativo { get; set; }

        [BsonElement(elementName: "CARGOS_FUNCIONARIOS")]
        public List<CargoFuncionarioQuery> CargosFuncionario { get; set; }
    }
}
