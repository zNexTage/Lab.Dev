using Features.Clientes;
using FluentAssertions;
using FluentAssertions.Extensions;
using Lab.Dev.Features.Tests.DadosHumanos;
using Moq;
using Xunit.Abstractions;

namespace Lab.Dev.Features.Tests.FluentAssertions
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteFluentAssertionsTests
    {
        readonly ClienteBogusFixtures _clienteFixtures;
        readonly ITestOutputHelper _outputHelper;


        public ClienteFluentAssertionsTests(ClienteBogusFixtures clienteFixtures, ITestOutputHelper outputHelper)
        {
            _clienteFixtures = clienteFixtures;
            _outputHelper = outputHelper;
        }


        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Categoria", "Cliente Fluent Assertion Testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrange
            var cliente = _clienteFixtures.GerarClienteValido();

            // Act
            var result = cliente.EhValido();

            // Assert 
            //Assert.True(result);
            //Assert.Equal(0, cliente.ValidationResult.Errors.Count);

            // Assert 
            result.Should().BeTrue();
            cliente.ValidationResult.Errors.Should().HaveCount(0);
        }

        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Categoria", "Cliente Fluent Assertion Testes")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = _clienteFixtures.GerarClienteInvalido();

            // Act
            var result = cliente.EhValido();

            // Assert 
            //Assert.False(result);
            //Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);

            // Assert 
            result.Should().BeFalse();
            cliente.ValidationResult.Errors.Should().HaveCountGreaterThanOrEqualTo(1, "deve possuir erros de validação");

            _outputHelper.WriteLine($"Foram encontrados {cliente.ValidationResult.Errors.Count} erros nesta validação");
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Fluent Assertion Testes")]
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
            clientes
                .Should()
                .HaveCountGreaterThanOrEqualTo(1)
                .And
                .OnlyHaveUniqueItems();

            clientes
                .Should()
                .NotContain(c => !c.Ativo);

            mocker.GetMock<IClienteRepository>()
                .Verify(r => r.ObterTodos(), Times.Once);

            clienteService
                .ExecutionTimeOf(c => c.ObterTodosAtivos())
                .Should()
                .BeLessThanOrEqualTo(1.Milliseconds(),
                 "apenas para demonstração."
                );
        }
    }
}
