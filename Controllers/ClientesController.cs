using apiTransferencia.DTOs;
using apiTransferencia.Services;
using Microsoft.AspNetCore.Mvc;

namespace apiTransferencia.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public IActionResult CadastrarCliente([FromBody] ClienteDTO clienteDto)
        {
            try
            {
                var cliente = _clienteService.Cadastrar(clienteDto);

                return CreatedAtAction(nameof(BuscarCliente), new { numeroConta = cliente.NumeroConta }, cliente);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("O número de conta já está em uso"))
                {
                    return Conflict(new { message = "Número de conta já cadastrado." });
                }

                return StatusCode(500, "Ocorreu um erro inesperado.");
            }
        }

        [HttpGet]
        public IActionResult ListarClientes()
        {
            var clientes = _clienteService.ListarTodos();
            return Ok(clientes);
        }

        [HttpGet("{numeroConta}")]
        public IActionResult BuscarCliente(string numeroConta)
        {
            try
            {
                var cliente = _clienteService.BuscarPorNumeroConta(numeroConta);
                return Ok(cliente);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);  
            }
        }
    }
}