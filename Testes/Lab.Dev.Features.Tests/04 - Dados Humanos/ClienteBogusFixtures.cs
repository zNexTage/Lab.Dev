using Bogus;
using Bogus.DataSets;
using Features.Clientes;
using Moq.AutoMock;

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

        public IEnumerable<Cliente> GerarClientesValidos(int quantidade, bool ativo)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var clientes = new Faker<Cliente>("pt_BR")
                .CustomInstantiator(f =>
                new Cliente(
                    Guid.NewGuid(),
                    f.Name.FirstName(genero),
                    f.Name.LastName(genero),
                    f.Date.Past(80, DateTime.Now.AddYears(-18)),
                    "",
                    ativo,
                    DateTime.Now
                ))
                // Define o email por fora apenas para utilizar o 
                // primeiro e segundo nome.
                .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.Nome.ToLower(), c.Sobrenome.ToLower()));

            return clientes.Generate(quantidade);
        }

        public IEnumerable<Cliente> ObterClientesVariados()
        {
            var clientes = new List<Cliente>();

            clientes.AddRange(GerarClientesValidos(50, true));
            clientes.AddRange(GerarClientesValidos(50, false));

            return clientes;
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

        public Tuple<ClienteService, AutoMocker> ObterClienteService()
        {
            var mocker = new AutoMocker();

            // Para mocker.CreateInstance, precisa ser a classe concreta. Queremos uma instância!!
            return Tuple.Create(mocker.CreateInstance<ClienteService>(), mocker);
        }

        public void Dispose()
        {
        }
    }
}
