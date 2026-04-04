using Features.Clientes;
using Lab.Dev.Features.Tests.DadosHumanos;
using MediatR;
using Moq;
using Xunit.Abstractions;

namespace Lab.Dev.Features.Tests.AutoMock
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteServiceAutoMockerTests
    {
        readonly ClienteBogusFixtures _clienteFixtures;
        readonly ITestOutputHelper _outputHelper;

        public ClienteServiceAutoMockerTests(ClienteBogusFixtures clienteFixtures, ITestOutputHelper outputHelper)
        {
            _clienteFixtures = clienteFixtures;
            _outputHelper = outputHelper;
        }


        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            //Arrange
            var cliente = _clienteFixtures.GerarClienteValido();

            (var clienteService, var mocker) = _clienteFixtures.ObterClienteService();
            
            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.True(cliente.EhValido());
            mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Once);
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            //Arrange
            var cliente = _clienteFixtures.GerarClienteInvalido();

            // Precisa ser a classe concreta. Queremos uma instância!!
            (var clienteService, var mocker) = _clienteFixtures.ObterClienteService();

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.False(cliente.EhValido());
            mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);

            // Equivalente a um Console.WriteLine
            _outputHelper.WriteLine($"Foram encontrados {cliente.ValidationResult.Errors.Count} erro(s) nesta validação.");
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange
            (var clienteService, var mocker) = _clienteFixtures.ObterClienteService();

            mocker.GetMock<IClienteRepository>()
                .Setup(c => c.ObterTodos())
                .Returns(_clienteFixtures.ObterClientesVariados());

            // Act
            var clientes = clienteService.ObterTodosAtivos();

            // Assert
            mocker.GetMock<IClienteRepository>()
                .Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.DoesNotContain(clientes, c => !c.Ativo);

            
        }
    }
}
