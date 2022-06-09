using CRUD_CLIENTE_MONGO.Entities;
using MongoDB.Driver;

namespace CRUD_CLIENTE_MONGO.Context
{
    public interface IClienteContext
    {
        IMongoCollection<Cliente> Cliente { get;  }
    }
}
