using CRUD_CLIENTE_MONGO.Context;
using CRUD_CLIENTE_MONGO.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD_CLIENTE_MONGO.Repositories
{
    //IMPLEMENTAÇÃO DA INTERFACE DE REPODITORIO DO CLIENTE
    public class ClienteRepository : IClienteRepository
    {
        private readonly IClienteContext _context;

        //INJEÇÃO DE DEPENDENCIA DO CONTEXTO DE CLIENTE
        public ClienteRepository(IClienteContext context)
        {
            _context = context;
        }

        public async Task CriarCliente(Cliente cadastro)
        {
            // VERIFICA SE JA EXISTE ALGUM CLIENTE CADASTRADO NO BANCO COM O MESMO CPF OU EMAIL
            var clienteDb = await _context.Cliente
                .Find(c => c.Cpf == cadastro.Cpf || c.Email == cadastro.Email)
                .FirstOrDefaultAsync();

            if (clienteDb != null)
                throw new Exception("Já existe um cliente para o Cpf ou Email informados");
            
            await _context.Cliente.InsertOneAsync(cadastro);
        }
                
        public async Task<bool> DeletarCliente(string cpf)
        {
            // FILTRO PARA LOCALIZAR O CLIENTE POR CPF
            var filter = Builders<Cliente>.Filter.Eq(c => c.Cpf, cpf);
            
            var deleteResult = await _context.Cliente.DeleteOneAsync(filter);

            return deleteResult.DeletedCount > 0;
        }

        public async Task<Cliente> GetClientePorCPF(string cpf)
        {
            return await _context.Cliente.Find(c => c.Cpf == cpf).FirstOrDefaultAsync();
        }

        public async Task<Cliente> GetClientePorNome(string nome)
        {
            return await _context.Cliente.Find(c => c.Nome == nome).FirstOrDefaultAsync();
        }

        public async Task<bool> AtualiarCliente(Cliente cadastro)
        {
            // LOCALIZA O CLIENTE POR ID E SUBSTITUI OS DADOS 
            var updateResult = await _context.Cliente.ReplaceOneAsync(
                filter: g => g.Id == cadastro.Id, replacement: cadastro);

            if(updateResult.MatchedCount == 0)
                throw new Exception("Nenhum cliente encontrado para o id informado");

            return updateResult.ModifiedCount > 0;
        }

        public async Task<IEnumerable<Cliente>> GetClientes()
        {
            return await _context.Cliente.Find(p => true).ToListAsync();
        }
    }
}
