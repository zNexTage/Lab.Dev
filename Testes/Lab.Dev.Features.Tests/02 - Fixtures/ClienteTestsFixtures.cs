using Features.Clientes;

namespace Lab.Dev.Features.Tests.Fixtures
{
    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection : ICollectionFixture<ClienteTestsFixtures>
    {

    }

    public class ClienteTestsFixtures : IDisposable
    {
        public Cliente GerarClienteValido()
        {
            return new Cliente(
                Guid.NewGuid(),
                "Christian",
                "Bueno",
                DateTime.Now.AddYears(-30),
                "chr@email.com",
                true,
                DateTime.Now
            );
        }

        public void Dispose()
        {
        }
    }
}
