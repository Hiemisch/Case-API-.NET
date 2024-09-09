using apiTransferencia.DTOs;
using apiTransferencia.Models;

namespace apiTransferencia.Services
{
    public interface IClienteService
    {
        Cliente Cadastrar(ClienteDTO clienteDto);
        Cliente BuscarPorNumeroConta(string numeroConta);
        IEnumerable<Cliente> ListarTodos();
    }
}
