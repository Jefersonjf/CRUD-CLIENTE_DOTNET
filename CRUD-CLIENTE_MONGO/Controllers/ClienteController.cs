using CRUD_CLIENTE_MONGO.Entities;
using CRUD_CLIENTE_MONGO.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD_CLIENTE_MONGO.Controllers
{
    // RECEBE REQUISIÇOES E ENTREGA RESPOSTAS (ENDPOINTS)

    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _repository;

        // INJEÇÃO DE DEPENDENCIA DO REPOSITORIO DE CLIENTE
        public ClienteController(IClienteRepository repository)
        {
            _repository = repository;
        }

        // RETORNA TODOS OS CLIENTES (Falta paginação)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            try
            {
                var clientes = await _repository.GetClientes();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // RETORNAR UM CLIENTE POR CPF
        [HttpGet("{cpf}")]
        public async Task<ActionResult<Cliente>> GetClientePorCpf(String cpf)
        {
            try
            {
                var cliente = await _repository.GetClientePorCPF(cpf);

                if (cliente == null)
                    throw new Exception("Cliente não encontrado para o cpf informado");

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // RETORNAR O CLIENTE POR NOME
        [HttpGet("Nome/{nome}")]
        public async Task<ActionResult<Cliente>> GetClientePorNome(String nome)
        {
            try
            {
                var cliente = await _repository.GetClientePorNome(nome);

                if (cliente == null)
                    throw new Exception("Cliente não encontrado para o nome informado");

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // CRIAR CLIENTE
        [HttpPost]
        public async Task<ActionResult<Cliente>> CriarCliente([FromBody] Cliente cliente)
        {
            try
            {
                if (cliente == null)
                    throw new Exception("Cliente inválido");

                cliente.Validar();

                await _repository.CriarCliente(cliente);
                var cadastroDb = await _repository.GetClientePorCPF(cliente.Cpf);

                return Ok(cadastroDb);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ATUALIZAR CLIENTE
        [HttpPut]
        public async Task<IActionResult> UpdateCliente([FromBody] Cliente cliente)
        {
            try
            {
                if (cliente is null)
                    return BadRequest("Cliente inválido");

                cliente.Validar();

                var result = await _repository.AtualiarCliente(cliente);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // DELETAR O CLIENTE POR CPF
        [HttpDelete("{cpf}")]
        public async Task<IActionResult> DeletarClientePorCpf(string cpf)
        {
            try
            {
                var result = await _repository.DeletarCliente(cpf);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
