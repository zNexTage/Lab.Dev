using Bogus;
using Bogus.DataSets;
using Features.Clientes;

namespace Lab.Dev.Features.Tests.DadosHumanos
{

    [CollectionDefinition(nameof(ClienteBogusCollection))]
    public class ClienteBogusCollection : ICollectionFixture<ClienteBogusFixtures>
    {
        
    }

    public class ClienteBogusFixtures : IDisposable
    {
        public Cliente GerarClienteValido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            // é possível definir os atributos dessa forma
            //var clienteFaker = new Faker<Cliente>();
            //clienteFaker.RuleFor(c => c.Nome, (f, c) => f.Name.FirstName());

            var cliente = new Faker<Cliente>("pt_BR")
                .CustomInstantiator(f =>
                new Cliente(
                    Guid.NewGuid(),
                    f.Name.FirstName(genero),
                    f.Name.LastName(genero),
                    f.Date.Past(80, DateTime.Now.AddYears(-18)),
                    "",
                    true,
                    DateTime.Now
                ))
                // Define o email por fora apenas para utilizar o 
                // primeiro e segundo nome.
                .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.Nome.ToLower(), c.Sobrenome.ToLower()));

            return cliente;
        }

        public Cliente GerarClienteInvalido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            // é possível definir os atributos dessa forma
            //var clienteFaker = new Faker<Cliente>();
            //clienteFaker.RuleFor(c => c.Nome, (f, c) => f.Name.FirstName());

            var cliente = new Faker<Cliente>("pt_BR")
                .CustomInstantiator(f =>
                new Cliente(
                    Guid.NewGuid(),
                    f.Name.FirstName(genero),
                    f.Name.LastName(genero),
                    DateTime.Now,
                    "",
                    true,
                    DateTime.Now
                ))
                // Define o email por fora apenas para utilizar o 
                // primeiro e segundo nome.
                .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.Nome.ToLower(), c.Sobrenome.ToLower()));

            return cliente;
        }

        public void Dispose()
        {
        }
    }
}
