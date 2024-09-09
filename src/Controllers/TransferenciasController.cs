using apiTransferencia.DTOs;
using apiTransferencia.Services;
using Microsoft.AspNetCore.Mvc;

namespace apiTransferencia.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class TransferenciasController : ControllerBase
    {
        private readonly ITransferenciaService _transferenciaService;

        public TransferenciasController(ITransferenciaService transferenciaService)
        {
            _transferenciaService = transferenciaService;
        }

        [HttpPost]
        public IActionResult RealizarTransferencia([FromBody] TransferenciaDTO transferenciaDto)
        {
            var sucesso = _transferenciaService.RealizarTransferencia(transferenciaDto);

            if (!sucesso)
                return BadRequest("Transferência não realizada. Verifique as informações e o saldo da conta.");

            return Ok("Transferência realizada com sucesso.");
        }

        [HttpGet("{numeroConta}")]
        public IActionResult ListarHistoricoTransferencias(string numeroConta)
        {
            var transferencias = _transferenciaService.ListarHistoricoTransferencias(numeroConta);
            return Ok(transferencias);
        }
    }
}
