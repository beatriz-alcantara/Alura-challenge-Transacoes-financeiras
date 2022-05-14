using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using ProvedorArmazenamentoIDMongo.Modelos;

namespace ProvedorArmazenamentoIDMongo.Repository
{
    public class RegrasUsuarioRepository : IRoleStore<RegrasUsuario>
    {
        private readonly IMongoCollection<RegrasUsuario> _regrasUsuarios;


        public RegrasUsuarioRepository(IOptions<MongoSettings> options)
        {
            var clienteMongo = new MongoClient(options.Value.ConnectionString);
            _regrasUsuarios = clienteMongo.GetDatabase(options.Value.Database).GetCollection<RegrasUsuario>("RegrasUsuario");
        }

        public async Task<IdentityResult> CreateAsync(RegrasUsuario role, CancellationToken cancellationToken)
        {
            await _regrasUsuarios.InsertOneAsync(role);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(RegrasUsuario role, CancellationToken cancellationToken)
        {
            await _regrasUsuarios.DeleteOneAsync(regra => regra.Id == role.Id);
            return IdentityResult.Success;
        }

        public void Dispose()
        {
        }

        public async Task<RegrasUsuario> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return (await _regrasUsuarios.FindAsync(regra => regra.Id == roleId)).FirstOrDefault();
        }

        public async Task<RegrasUsuario> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return (await _regrasUsuarios.FindAsync(item => item.Name == normalizedRoleName)).FirstOrDefault();
        }

        public async Task<string> GetNormalizedRoleNameAsync(RegrasUsuario role, CancellationToken cancellationToken)
        {
            return (await _regrasUsuarios.FindAsync(item => item.Id == role.Id)).FirstOrDefault().Name;
        }

        public async Task<string> GetRoleIdAsync(RegrasUsuario role, CancellationToken cancellationToken)
        {
            return (await _regrasUsuarios.FindAsync(item => item.Name == role.Name)).FirstOrDefault().Id;
        }

        public async Task<string> GetRoleNameAsync(RegrasUsuario role, CancellationToken cancellationToken)
        {
            return (await _regrasUsuarios.FindAsync(item => item.Id == role.Id)).FirstOrDefault().Name;
        }

        public async Task SetNormalizedRoleNameAsync(RegrasUsuario role, string normalizedName, CancellationToken cancellationToken)
        {
            role.Name = normalizedName;
            await _regrasUsuarios.InsertOneAsync(role);
        }

        public async Task SetRoleNameAsync(RegrasUsuario role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            await _regrasUsuarios.InsertOneAsync(role);
        }

        public async Task<IdentityResult> UpdateAsync(RegrasUsuario role, CancellationToken cancellationToken)
        {
            await _regrasUsuarios.InsertOneAsync(role);
            return IdentityResult.Success;
        }
    }
}
