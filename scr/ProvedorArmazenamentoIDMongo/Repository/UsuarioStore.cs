using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProvedorArmazenamentoIDMongo.Modelos;

namespace ProvedorArmazenamentoIDMongo
{
    public class UsuarioStore : IUserStore<Usuarios>
    {
        private IMongoDatabase _database;

        public UsuarioStore(IOptions<MongoSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            _database = mongoClient.GetDatabase(settings.Value.Database);
        }
        public async Task<IdentityResult> CreateAsync(Usuarios user, CancellationToken cancellationToken)
        {
            await _database.GetCollection<Usuarios>("Usuarios").InsertOneAsync(user);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Usuarios user, CancellationToken cancellationToken)
        {
            await _database.GetCollection<Usuarios>("Usuarios").DeleteOneAsync(item => item.Email == user.Email);
            return IdentityResult.Success;
        }

        public void Dispose()
        {
        }

        public async Task<Usuarios> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return (await _database.GetCollection<Usuarios>("Usuarios").FindAsync(item => item.Id == userId)).FirstOrDefault();
        }

        public async Task<Usuarios> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return (await _database.GetCollection<Usuarios>("Usuarios").FindAsync(item => item.NormalizedUserName == normalizedUserName)).FirstOrDefault();
        }

        public async Task<string> GetNormalizedUserNameAsync(Usuarios user, CancellationToken cancellationToken)
        {
            return (await _database.GetCollection<Usuarios>("Usuarios").FindAsync(item => item.Id == user.Id)).FirstOrDefault()?.NormalizedUserName ?? "";
        }

        public async Task<string> GetUserIdAsync(Usuarios user, CancellationToken cancellationToken)
        {
            return (await _database.GetCollection<Usuarios>("Usuarios").FindAsync(item => item.Email == user.Email)).FirstOrDefault()?.Id ?? "";
        }

        public async Task<string> GetUserNameAsync(Usuarios user, CancellationToken cancellationToken)
        {
            return (await _database.GetCollection<Usuarios>("Usuarios").FindAsync(item => item.Id == user.Id)).FirstOrDefault()?.UserName ?? "";
        }

        public async Task SetNormalizedUserNameAsync(Usuarios user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            await _database.GetCollection<Usuarios>("Usuarios").ReplaceOneAsync(item => item.Id == user.Id, user);
        }

        public async Task SetUserNameAsync(Usuarios user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            await _database.GetCollection<Usuarios>("Usuarios").ReplaceOneAsync(item => item.Id == user.Id, user);
        }

        public async Task<IdentityResult> UpdateAsync(Usuarios user, CancellationToken cancellationToken)
        {
            await _database.GetCollection<Usuarios>("Usuarios").ReplaceOneAsync(item => item.Id == user.Id, user);
            return IdentityResult.Success;
        }
    }
}