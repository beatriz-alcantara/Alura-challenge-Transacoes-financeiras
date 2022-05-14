using Alura_Challange_Transacao_Financeira.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProvedorArmazenamentoIDMongo.Modelos;

namespace Alura_Challange_Transacao_Financeira.Repository
{
    
    public class ImportacoesRepository
    {
        private IMongoCollection<Importacao> _collection;
        public ImportacoesRepository(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _collection = client.GetDatabase(settings.Value.Database).GetCollection<Importacao>("Importacoes");
        }

        public async Task AddRegistro(Importacao importacao)
        {
            await _collection.InsertOneAsync(importacao);
        }

        public async Task<IList<Importacao>> GetAllRegistros()
        {
            return await _collection.AsQueryable().ToListAsync();
        }
    }
}
