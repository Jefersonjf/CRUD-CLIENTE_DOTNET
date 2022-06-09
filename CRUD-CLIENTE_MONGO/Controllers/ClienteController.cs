using CRUD_CLIENTE_MONGO.Entities;
using CRUD_CLIENTE_MONGO.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD_CLIENTE_MONGO.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _repository;

        public ClienteController(IClienteRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var clientes = await _repository.GetClientes();
            return Ok(clientes);
        }

        [HttpGet("{cpf}")]
        public async Task<ActionResult<Cliente>> GetClientePorCpf(String cpf)
        {
            var cliente = await _repository.GetClientePorCPF(cpf);

            if (cliente == null)
            {
                return null;
            }

            return Ok(cliente);
        }

        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<Cliente>> GetClientePorNome(String nome)
        {
            var cliente = await _repository.GetClientePorNome(nome);

            if (cliente == null)
            {
                return null;
            }

            return Ok(cliente);
        }


        [HttpPost]
        public async Task<ActionResult<Cliente>> CriarCliente([FromBody] Cliente cliente)
        {
            try
            {
                if (cliente is null)
                    throw new Exception("Cliente inválido");

                cliente.Validar();

                await _repository.CriarCliente(cliente);
                var cadastroDb = await _repository.GetClientePorCPF(cliente.Cpf);

                return Ok(cadastroDb);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCliente([FromBody] Cliente cliente)
        {
            if (cliente is null)
                return BadRequest("Cliente inválido");

            cliente.Validar();

            var result = await _repository.AtualiarCliente(cliente);

            return Ok(result);
        }

        [HttpDelete("{cpf}")]
        public async Task<IActionResult> DeletarClientePorCpf(string cpf)
        {
            var result = await _repository.DeletarCliente(cpf);
            return Ok(result);
        }
    }
}
