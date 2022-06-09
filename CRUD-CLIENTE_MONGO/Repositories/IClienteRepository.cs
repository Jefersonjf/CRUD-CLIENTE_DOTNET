using CRUD_CLIENTE_MONGO.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD_CLIENTE_MONGO.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetClientes();
        Task<Cliente> GetClientePorCPF(string cpf);
        Task<Cliente> GetClientePorNome(string name);
        Task CriarCliente (Cliente cadastro);
        Task<bool> AtualiarCliente(Cliente cadastro);
        Task<bool> DeletarCliente (string CPF);
    }
}
