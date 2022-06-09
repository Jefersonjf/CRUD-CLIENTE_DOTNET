using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CRUD_CLIENTE_MONGO.Entities
{
    public class Cliente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Endereço { get; set; }
        public string Telefone { get; set; }
        public bool Status { get; set; }

        public void Validar()
        {
            if (Cpf.Length != 11)
            {
                throw new Exception("O CPF deve conter 11 digitos");
            }

            if (!Email.Contains("@"))
            {
                throw new Exception("Email inválido");
            }



        }


    }
}
