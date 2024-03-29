using MigracaoWorkerService.Context;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Models.Query
{
    [BsonCollection("CARGO_FUNCIONARIO")]
    public class CargoFuncionarioQuery : Event
    {
        [BsonElement(elementName: "CGFN_ID")]
        public override int Id { get; set; }

        [BsonElement(elementName: "CGFN_ID_FUNCIONARIO")]
        public int IdFuncionario { get; set; }

        [BsonElement(elementName: "FUNCIONARIO")]
        public FuncionarioQuery Funcionario { get; set; }

        [BsonElement(elementName: "CGFN_ID_CARGO")]
        public int IdCargo { get; set; }

        [BsonElement(elementName: "CARGO")]
        public CargoQuery Cargo { get; set; }

        [BsonElement(elementName: "CGFN_DATA_INICIO")]
        public DateTime DataInicio { get; set; }

        [BsonElement(elementName: "CGFN_DATA_FIM")]
        public DateTime? DataFim { get; set; }
    }
}
