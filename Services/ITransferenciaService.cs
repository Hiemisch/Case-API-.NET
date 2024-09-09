using apiTransferencia.DTOs;
using apiTransferencia.Models;

namespace apiTransferencia.Services
{
    public interface ITransferenciaService
    {
        bool RealizarTransferencia(TransferenciaDTO transferenciaDto);
        IEnumerable<Transferencia> ListarHistoricoTransferencias(string numeroConta);
    }
}
