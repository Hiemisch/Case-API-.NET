using apiTransferencia.Models;

namespace apiTransferencia.Repositories
{
    public interface ITransferenciaRepository
    {
        void Registrar(Transferencia transferencia);
        IEnumerable<Transferencia> ListarPorNumeroConta(string numeroConta);
    }
}
