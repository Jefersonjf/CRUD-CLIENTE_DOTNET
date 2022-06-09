using CRUD_CLIENTE_MONGO.Context;
using CRUD_CLIENTE_MONGO.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD_CLIENTE_MONGO.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IClienteContext _context;
        public ClienteRepository(IClienteContext context)
        {
            _context = context;
        }

        public async Task CriarCliente(Cliente cadastro)
        {
            var clienteDb = await _context.Cliente
                .Find(p => p.Cpf == cadastro.Cpf || p.Email == cadastro.Email)
                .FirstOrDefaultAsync();

            if (clienteDb != null)
            {
                throw new Exception("Já existe um cliente para o Cpf ou Email informados");
            }

            await _context.Cliente.InsertOneAsync(cadastro);
        }

        public async Task<bool> DeletarCliente(string CPF)
        {
            FilterDefinition<Cliente> filter = Builders<Cliente>.Filter.Eq(p => p.Cpf, CPF);

            DeleteResult deleteResult = await _context.Cliente.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<Cliente> GetClientePorCPF(string cpf)
        {
            return await _context.Cliente.Find(p => p.Cpf == cpf).FirstOrDefaultAsync();
        }

        public async Task<Cliente> GetClientePorNome(string nome)
        {
            return await _context.Cliente.Find(p => p.Name == nome).FirstOrDefaultAsync();
        }

        public async Task<bool> AtualiarCliente(Cliente cadastro)
        {
            var updateResult = await _context.Cliente.ReplaceOneAsync(
                filter: g => g.Id == cadastro.Id, replacement: cadastro);

            return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
        }

        public async Task<IEnumerable<Cliente>> GetClientes()
        {
            return await _context.Cliente.Find(p => true).ToListAsync();
        }
    }
}
