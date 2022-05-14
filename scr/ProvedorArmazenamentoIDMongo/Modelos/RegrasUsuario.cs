using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvedorArmazenamentoIDMongo.Modelos
{
    public class RegrasUsuario
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
