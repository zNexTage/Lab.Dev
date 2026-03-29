using Features.Clientes;

namespace Lab.Dev.Features.Tests.Fixtures
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteFixturesTest
    {
        readonly ClienteTestsFixtures _clienteTestsFixture;

        public ClienteFixturesTest(ClienteTestsFixtures clienteTestsFixtures)
        {
            _clienteTestsFixture = clienteTestsFixtures;
        }

        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Categoria", "Cliente trait testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteValido();

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.True(result);
            Assert.Empty(cliente.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Categoria", "Cliente trait testes")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = new Cliente(
                Guid.NewGuid(),
                "Christian",
                "Bueno",
                DateTime.Now,
                "chr2.com",
                true,
                DateTime.Now
            );

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.False(result);
            Assert.NotEmpty(cliente.ValidationResult.Errors);
        }
    }
}
