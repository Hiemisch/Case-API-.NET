using apiTransferencia.Data;
using apiTransferencia.Models;

namespace apiTransferencia.Repositories
{
    public class TransferenciaRepository : ITransferenciaRepository
    {
        private readonly BankingContext _context;

        public TransferenciaRepository(BankingContext context)
        {
            _context = context;
        }

        public void Registrar(Transferencia transferencia)
        {
            _context.Transferencias.Add(transferencia);
            _context.SaveChanges();
        }

        public IEnumerable<Transferencia> ListarPorNumeroConta(string numeroConta)
        {
            return _context.Transferencias
                           .Where(t => t.NumeroContaOrigem == numeroConta || t.NumeroContaDestino == numeroConta)
                           .OrderByDescending(t => t.DataTransferencia)
                           .ToList();
        }
    }
}
