using Xunit;
using Moq;
using System;
using apiTransferencia.DTOs;
using apiTransferencia.Models;
using apiTransferencia.Repositories;
using apiTransferencia.Services;

namespace apiTransferencia.apiTransferencia.Tests.Services
{


    public class ClienteServiceTests
    {
        private readonly ClienteService _clienteService;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock = new Mock<IClienteRepository>();

        public ClienteServiceTests()
        {
            _clienteService = new ClienteService(_clienteRepositoryMock.Object);
        }

        [Fact]
        public void CadastrarCliente_DeveRetornarClienteCriado()
        {
            var clienteDto = new ClienteDTO { Nome = "Teste", NumeroConta = "12345", Saldo = 1000m };
            var cliente = new Cliente { Id = Guid.NewGuid(), Nome = "Teste", NumeroConta = "12345", Saldo = 1000m };

            _clienteRepositoryMock.Setup(r => r.Cadastrar(It.IsAny<Cliente>())).Returns(cliente);

            var resultado = _clienteService.Cadastrar(clienteDto);

            Assert.Equal(cliente.Nome, resultado.Nome);
            Assert.Equal(cliente.NumeroConta, resultado.NumeroConta);
            Assert.Equal(cliente.Saldo, resultado.Saldo);
        }

        [Fact]
        public void BuscarCliente_DeveRetornarCliente()
        {
            var numeroConta = "12345";
            var cliente = new Cliente { Id = Guid.NewGuid(), Nome = "Teste", NumeroConta = numeroConta, Saldo = 1000m };

            _clienteRepositoryMock.Setup(r => r.BuscarPorNumeroConta(numeroConta)).Returns(cliente);

            var resultado = _clienteService.BuscarPorNumeroConta(numeroConta);

            Assert.NotNull(resultado);
            Assert.Equal(cliente.NumeroConta, resultado.NumeroConta);
        }
    }
}
