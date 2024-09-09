using Xunit;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using apiTransferencia.DTOs;
using FluentAssertions;

namespace apiTransferencia.Tests.Integration
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

    public class ClientesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;



        public ClientesControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_CadastrarCliente_DeveRetornarSucessoSeContaNaoExistir()
        {
            var clienteDto = new ClienteDTO { Nome = "Teste Sucesso", NumeroConta = "123456", Saldo = 2000m };
            var content = new StringContent(JsonSerializer.Serialize(clienteDto), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/clientes", content);

            response.EnsureSuccessStatusCode(); // Verifica se o status é 2xx
            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Contain("Teste Sucesso");
        }

        [Fact]
        public async Task Post_CadastrarCliente_DeveRetornarErroSeContaExistir()
        {
            var clienteDto = new ClienteDTO { Nome = "Teste Duplicado", NumeroConta = "12345", Saldo = 1000m };
            var content = new StringContent(JsonSerializer.Serialize(clienteDto), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/clientes", content);
            var responseDuplicado = await _client.PostAsync("/api/v1/clientes", content);

            responseDuplicado.StatusCode.Should().Be(System.Net.HttpStatusCode.Conflict);
        }



        [Fact]
        public async Task Get_ListarClientes_DeveRetornarListaClientes()
        {
            var response = await _client.GetAsync("/api/v1/clientes");

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().NotBeNullOrEmpty();
        }
    }
}


