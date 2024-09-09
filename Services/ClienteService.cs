using apiTransferencia.DTOs;
using apiTransferencia.Models;
using apiTransferencia.Repositories;

namespace apiTransferencia.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public Cliente Cadastrar(ClienteDTO clienteDto)
        {
            var clienteExistente = _clienteRepository.BuscarPorNumeroConta(clienteDto.NumeroConta);
            if (clienteExistente != null)
            {
                throw new Exception("O número de conta já está em uso.");
            }

            var cliente = new Cliente
            {
                Nome = clienteDto.Nome,
                NumeroConta = clienteDto.NumeroConta,
                Saldo = clienteDto.Saldo
            };

            _clienteRepository.Cadastrar(cliente);
            return cliente;
        }


        public Cliente BuscarPorNumeroConta(string numeroConta)
        {
            var cliente = _clienteRepository.BuscarPorNumeroConta(numeroConta);
            if (cliente == null)
            {
                throw new KeyNotFoundException("Cliente não encontrado");
            }

            return cliente;
        }

        public IEnumerable<Cliente> ListarTodos()
        {
            return _clienteRepository.ListarTodos();
        }
    }
}
