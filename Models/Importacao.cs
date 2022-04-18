using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Alura_Challange_Transacao_Financeira.Models
{
    public class Importacao
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string DataImportacao { get; set; }
        public string DataTransacoes{ get; set; }
    }
}
