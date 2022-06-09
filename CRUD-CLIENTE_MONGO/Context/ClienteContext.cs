using CRUD_CLIENTE_MONGO.Entities;
using MongoDB.Driver;
using System;

namespace CRUD_CLIENTE_MONGO.Context
{
    public class ClienteContext : IClienteContext
    {
        public ClienteContext()
        {
            var connectionString = Environment.GetEnvironmentVariable("MongoConnectionString");

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("crud-cliente");

            Cliente = database.GetCollection<Cliente>("cliente");
        }

        public IMongoCollection<Cliente> Cliente { get; }
         
    }
}
