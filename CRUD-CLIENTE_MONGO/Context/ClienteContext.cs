using CRUD_CLIENTE_MONGO.Entities;
using MongoDB.Driver;
using System;

namespace CRUD_CLIENTE_MONGO.Context
{
    //IMPLEMENTAÇÃO DA INTERFACE DE CONTEXTO DE CLIENTE
    public class ClienteContext : IClienteContext
    {
        public IMongoCollection<Cliente> Cliente { get; }

        public ClienteContext()
        {
            var connectionString = Environment.GetEnvironmentVariable("MongoConnectionString");

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("crud-cliente");

            Cliente = database.GetCollection<Cliente>("cliente");
        }
        
    }
}
