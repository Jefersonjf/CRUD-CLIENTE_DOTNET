using CRUD_CLIENTE_MONGO.Entities;
using MongoDB.Driver;

namespace CRUD_CLIENTE_MONGO.Context
{
    //Interface de contexto de cliente 
    public interface IClienteContext
    {
        IMongoCollection<Cliente> Cliente { get; }
    }
}
