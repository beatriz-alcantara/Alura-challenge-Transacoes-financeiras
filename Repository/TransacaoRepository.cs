using Alura_Challange_Transacao_Financeira.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Alura_Challange_Transacao_Financeira.Repository
{
    public class TransacaoRepository
    {
        public IMongoDatabase database { get; set; }
        public TransacaoRepository(IOptions<MongoSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            database = mongoClient.GetDatabase(settings.Value.Database);
        }

        public async void CreateCollection(IList<Transacao> transacoes)
        {
            await database.CreateCollectionAsync(transacoes.First().DataHora.Date.ToString());
            var collection = database.GetCollection<Transacao>(transacoes.First().DataHora.Date.ToString());
            await collection.InsertManyAsync(transacoes);
        }

        public bool CollectionExist(string collectionName)
        {
            return database.ListCollectionNames().ToEnumerable().Any(nome => nome == collectionName);
        }
    }
}
