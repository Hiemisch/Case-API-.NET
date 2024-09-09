using apiTransferencia.Models;

namespace apiTransferencia.Repositories
{
    public interface IClienteRepository
    {
        Cliente Cadastrar(Cliente cliente);
        Cliente BuscarPorNumeroConta(string numeroConta);
        IEnumerable<Cliente> ListarTodos();
        void Atualizar(Cliente cliente);
    }
}
