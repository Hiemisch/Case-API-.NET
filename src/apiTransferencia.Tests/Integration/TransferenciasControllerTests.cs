using Xunit;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using apiTransferencia;
using apiTransferencia.DTOs;
using FluentAssertions;

namespace apiTransferencia.apiTransferencia.Tests.Integration
{
    public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(GetProjectContentRoot());
        }
        private string GetProjectContentRoot()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var projectDir = Path.Combine(currentDir, @"..\..\..\");
            return Path.GetFullPath(projectDir);
        }
    }
    public class TransferenciasControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public TransferenciasControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_RealizarTransferencia_DeveRetornarSucesso()
        {
            var cliente1 = new ClienteDTO { Nome = "Teste1", NumeroConta = "12345", Saldo = 5000 };
            var cliente2 = new ClienteDTO { Nome = "Teste2", NumeroConta = "67890", Saldo = 2000 };
            await _client.PostAsync("/api/clientes", new StringContent(JsonSerializer.Serialize(cliente1), Encoding.UTF8, "application/json"));
            await _client.PostAsync("/api/clientes", new StringContent(JsonSerializer.Serialize(cliente2), Encoding.UTF8, "application/json"));

            var transferenciaDto = new TransferenciaDTO { NumeroContaOrigem = "12345", NumeroContaDestino = "67890", Valor = 1000 };
            var content = new StringContent(JsonSerializer.Serialize(transferenciaDto), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/transferencias", content);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Contain("sucesso");
        }

        [Fact]
        public async Task Get_ListarHistoricoTransferencias_DeveRetornarHistorico()
        {
            var response = await _client.GetAsync("/api/transferencias/12345");

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNullOrEmpty();
        }
    }
}
