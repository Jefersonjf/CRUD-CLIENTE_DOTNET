using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.RegularExpressions;

namespace CRUD_CLIENTE_MONGO.Entities
{
    public class Cliente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public bool Status { get; set; }

        public void Validar()
        {
            if (Cpf == null || Cpf.Length != 11)
                throw new Exception("O cpf deve conter 11 digitos");                             

            if (!Regex.IsMatch(Cpf, @"^[0-9]+$"))
                throw new Exception("O cpf deve conter apenas numero");

            if (Email == null || !Email.Contains("@"))
                throw new Exception("Email inválido");

            if (string.IsNullOrEmpty(Nome))
                throw new Exception("Nome inválido");

            if (string.IsNullOrEmpty(Endereco))
                throw new Exception("Endereco inválido");

            if (string.IsNullOrEmpty(Telefone))
                throw new Exception("Telefone inválido");
        }

    }
}
