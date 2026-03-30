# Dados humanos

Permitem enriquecer os testes, pois possibilitam trabalhar com dados gerados de forma mais próxima da realidade, tornando os cenários mais representativos do uso real da aplicação.

Além de deixar os testes mais realistas, o uso de dados humanos também ajuda a:

- validar regras de negócio com dados variados;
- evitar dependência excessiva de valores fixos;
- simular diferentes perfis de entrada, como nomes, e-mails, telefones, endereços e datas;
- identificar falhas que podem passar despercebidas com dados muito simples ou repetitivos;
tornar os testes menos artificiais e mais próximos do comportamento esperado em produção.

Também é importante ter cuidado, pois dados aleatórios podem dificultar a reprodutibilidade dos testes. Por isso, o ideal é usar esse tipo de dado quando ele agrega valor ao cenário, mantendo previsibilidade quando necessário.

# Quando esse tipo de dado é útil

Você pode dizer que dados humanos são especialmente úteis em cenários como:

- testes de cadastro;
- validação de formulários;
- geração de entidades para testes automatizados;
- testes de integração;
- testes em massa com grande volume de registros.

# Cuidados no uso

Esse ponto vale bastante a pena mencionar:

- não usar aleatoriedade sem controle em testes que precisam ser determinísticos;
- evitar depender de valores gerados que mudam a cada execução sem necessidade;
- garantir que os dados gerados respeitem regras do domínio;
- lembrar que dados “parecidos com reais” não substituem casos de borda.

Por exemplo, além de gerar nomes e e-mails válidos, ainda é preciso testar:

- campos vazios;
- tamanhos máximos;
- caracteres especiais;
- formatos inválidos;
- duplicidade de informações.

# Framework

- Bogus;
    - `Install-Package Bogus`