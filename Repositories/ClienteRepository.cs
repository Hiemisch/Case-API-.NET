using apiTransferencia.Data;
using apiTransferencia.Models;

namespace apiTransferencia.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly BankingContext _context;

        public ClienteRepository(BankingContext context)
        {
            _context = context;
        }

        public Cliente Cadastrar(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
            return cliente;
        }

        public Cliente BuscarPorNumeroConta(string numeroConta)
        {
            return _context.Clientes.SingleOrDefault(c => c.NumeroConta == numeroConta);
        }

        public IEnumerable<Cliente> ListarTodos()
        {
            return _context.Clientes.ToList();
        }

        public void Atualizar(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            _context.SaveChanges();
        }
    }
}
