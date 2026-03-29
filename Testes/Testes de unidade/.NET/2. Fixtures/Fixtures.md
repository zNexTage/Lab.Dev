# Fixtures

Fixture é um objeto ou estrutura usada para preparar um cenário compartilhado que vários testes precisam usar. **Em vez de repetir setup em todo teste, você centraliza a criação desse contexto em um lugar só. Isso é útil quando a preparação é cara ou trabalhosa, como abrir conexão com banco, subir um container, montar dados de teste, instanciar um HttpClient, criar arquivos temporários ou inicializar um servidor de teste.**

**O ideal é que a fixture represente mais a infraestrutura compartilhada do que dados mutáveis do teste.**

Antes de entrar em `IClassFixture` e `ICollectionFixture`, deve-se entender o seguinte:

`class fixture compartilha por classe; collection fixture compartilha por coleção.`

## IClassFixture<T>

- Compartilha uma única instância da fixture entre os testes de uma mesma classe;
- a fixture é criada antes da execução dos testes da classe;
- a fixture é descartada ao final da execução da classe.

Se `TestesA` e `TestesB` implementam `IClassFixture<MinhaFixture>`, cada classe recebe sua própria instância de `MinhaFixture`. Portanto, `TestesA` usa uma instância e `TestesB` usa outra.

## ICollectionFixture<T>

- uma instância da fixture para todas as classes que pertencem à mesma collection (todas marcadas com a mesma `[Collection("...")]`).
- Ela é criada antes dos testes da collection;
- É descartada ao final da collection;
- Várias classes de teste podem compartilhar a mesma instância da fixture, desde que pertençam à mesma collection.

Se `TestesA` e `TestesB` estiverem na mesma collection, e essa collection usar `ICollectionFixture<MinhaFixture>`, então:

`TestesA` e `TestesB` compartilham a mesma instância de `MinhaFixture`.

`ICollectionFixture<T>` faz sentido quando várias classes precisam compartilhar um mesmo recurso caro ou centralizado, por exemplo:

- banco de dados de teste;
- container Docker;
- servidor de teste;
- WebApplicationFactory;
- dados iniciais comuns.


# Como decidir na prática

Faça estas perguntas:

1. Só uma classe precisa desse contexto?
Se sim, use IClassFixture.

2. Mais de uma classe precisa exatamente do mesmo contexto?
Se sim, use ICollectionFixture.

3. Compartilhar entre classes pode causar interferência?
Se sim, prefira IClassFixture.

4. O setup é muito caro e vale a pena reaproveitar entre várias classes?
Se sim, pense em ICollectionFixture.

`IClassFixture`

Imagine:

`UsuarioServiceTests`
`ProdutoServiceTests`

Cada uma pode ter sua própria fixture, porque cada classe testa uma área diferente.

`ICollectionFixture`

Agora imagine:

`UsuarioRepositoryTests`
`ProdutoRepositoryTests`
`PedidoRepositoryTests`

Todas usam o mesmo banco de integração.
Nesse caso, faz sentido colocar as três na mesma collection e compartilhar a mesma fixture.