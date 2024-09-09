using apiTransferencia.DTOs;
using apiTransferencia.Models;
using apiTransferencia.Repositories;
using Microsoft.EntityFrameworkCore;

namespace apiTransferencia.Services
{
    public class TransferenciaService : ITransferenciaService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ITransferenciaRepository _transferenciaRepository;
        private static readonly object _lock = new object();

        public TransferenciaService(IClienteRepository clienteRepository, ITransferenciaRepository transferenciaRepository)
        {
            _clienteRepository = clienteRepository;
            _transferenciaRepository = transferenciaRepository;
        }

        public bool RealizarTransferencia(TransferenciaDTO transferenciaDto)
        {
            var contaOrigem = _clienteRepository.BuscarPorNumeroConta(transferenciaDto.NumeroContaOrigem);
            var contaDestino = _clienteRepository.BuscarPorNumeroConta(transferenciaDto.NumeroContaDestino);

            if (contaOrigem == null || contaDestino == null || transferenciaDto.Valor <= 0 || transferenciaDto.Valor > 10000 || contaOrigem.Saldo < transferenciaDto.Valor)
            {
                RegistrarTransferencia(transferenciaDto, false);
                return false;
            }

            try
            {
                contaOrigem.Saldo -= transferenciaDto.Valor;
                contaDestino.Saldo += transferenciaDto.Valor;

                _clienteRepository.Atualizar(contaOrigem);
                _clienteRepository.Atualizar(contaDestino);

                RegistrarTransferencia(transferenciaDto, true);
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("Conflito de concorrência detectado ao tentar realizar a transferência.");
            }
        }

        private void RegistrarTransferencia(TransferenciaDTO transferenciaDto, bool sucesso)
        {
            var transferencia = new Transferencia
            {
                Id = Guid.NewGuid(),
                NumeroContaOrigem = transferenciaDto.NumeroContaOrigem,
                NumeroContaDestino = transferenciaDto.NumeroContaDestino,
                Valor = transferenciaDto.Valor,
                DataTransferencia = DateTime.UtcNow,
                Sucesso = sucesso
            };

            _transferenciaRepository.Registrar(transferencia);
        }

        public IEnumerable<Transferencia> ListarHistoricoTransferencias(string numeroConta)
        {
            return _transferenciaRepository.ListarPorNumeroConta(numeroConta);
        }
    }
}
