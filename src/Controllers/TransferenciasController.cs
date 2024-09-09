using apiTransferencia.DTOs;
using apiTransferencia.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                var sucesso = _transferenciaService.RealizarTransferencia(transferenciaDto);
                if (!sucesso)
                {
                    return BadRequest("Transferência inválida. Verifique os detalhes da conta e o saldo.");
                }

                return Ok("Transferência realizada com sucesso.");
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Erro de concorrência detectado. A transferência não pôde ser concluída porque as informações da conta foram alteradas por outro processo.");
            }
        }

        [HttpGet("{numeroConta}")]
        public IActionResult ListarHistoricoTransferencias(string numeroConta)
        {
            var transferencias = _transferenciaService.ListarHistoricoTransferencias(numeroConta);
            return Ok(transferencias);
        }
    }
}
