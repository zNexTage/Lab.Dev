namespace Lab.Dev.Features.Tests.DadosHumanos
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteBogusTestes
    {
        readonly ClienteBogusFixtures _clienteTestsFixture;

        public ClienteBogusTestes(ClienteBogusFixtures clienteTestsFixtures)
        {
            _clienteTestsFixture = clienteTestsFixtures;
        }

        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Categoria", "Cliente bogus testes")]
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
        [Trait("Categoria", "Cliente bogus testes")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteInvalido();

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.False(result);
            Assert.NotEmpty(cliente.ValidationResult.Errors);
        }
    }
}
