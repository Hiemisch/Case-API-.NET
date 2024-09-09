using Xunit;
using Moq;
using apiTransferencia.DTOs;
using apiTransferencia.Models;
using apiTransferencia.Repositories;
using apiTransferencia.Services;

namespace apiTransferencia.apiTransferencia.Tests.Services
{
    public class TransferenciaServiceTests
    {
        private readonly TransferenciaService _transferenciaService;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock = new Mock<IClienteRepository>();
        private readonly Mock<ITransferenciaRepository> _transferenciaRepositoryMock = new Mock<ITransferenciaRepository>();

        public TransferenciaServiceTests()
        {
            _transferenciaService = new TransferenciaService(_clienteRepositoryMock.Object, _transferenciaRepositoryMock.Object);
        }

        [Fact]
        public void RealizarTransferencia_DeveRetornarFalseSeSaldoInsuficiente()
        {
            var transferenciaDto = new TransferenciaDTO { NumeroContaOrigem = "12345", NumeroContaDestino = "67890", Valor = 500};
            var contaOrigem = new Cliente { Id = Guid.NewGuid(), Nome = "Teste", NumeroConta = "12345", Saldo = 100};
            var contaDestino = new Cliente { Id = Guid.NewGuid(), Nome = "Teste2", NumeroConta = "67890", Saldo = 200};

            _clienteRepositoryMock.Setup(r => r.BuscarPorNumeroConta("12345")).Returns(contaOrigem);
            _clienteRepositoryMock.Setup(r => r.BuscarPorNumeroConta("67890")).Returns(contaDestino);

            var resultado = _transferenciaService.RealizarTransferencia(transferenciaDto);

            Assert.False(resultado);
            _transferenciaRepositoryMock.Verify(r => r.Registrar(It.IsAny<Transferencia>()), Times.Once);
        }

        [Fact]
        public void RealizarTransferencia_DeveRetornarTrueSeSaldoSuficiente()
        {
            var transferenciaDto = new TransferenciaDTO { NumeroContaOrigem = "12345", NumeroContaDestino = "67890", Valor = 100};
            var contaOrigem = new Cliente { Id = Guid.NewGuid(), Nome = "Teste", NumeroConta = "12345", Saldo = 500};
            var contaDestino = new Cliente { Id = Guid.NewGuid(), Nome = "Teste2", NumeroConta = "67890", Saldo = 200};

            _clienteRepositoryMock.Setup(r => r.BuscarPorNumeroConta("12345")).Returns(contaOrigem);
            _clienteRepositoryMock.Setup(r => r.BuscarPorNumeroConta("67890")).Returns(contaDestino);

            var resultado = _transferenciaService.RealizarTransferencia(transferenciaDto);

            Assert.True(resultado);
            _clienteRepositoryMock.Verify(r => r.Atualizar(It.IsAny<Cliente>()), Times.Exactly(2));
            _transferenciaRepositoryMock.Verify(r => r.Registrar(It.IsAny<Transferencia>()), Times.Once);
        }
    }
}