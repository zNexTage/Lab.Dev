# Tipos de testes mais comuns

## Testes de unidade

- Testa um código, um comportamento, etc.
- Teste de uma controller, serviço, regra de negócio;
- Através do input A deve ser gerado um output B;
- Testa uma única unidade do sistema. Realiza o teste de maneira isolada, geralmente simulando as prováveis dependências que aquela unidade tem;
- É comum que a unidade seja uma classe;
- Teste de unidade não bate em infra.

## Testes de integração

- Testa a funcionalidade de ponta a ponta;

## Testes automatizados

- Testes que rodam de forma autônoma;
- Ex: selenium;
- Utiliza uma ferramenta que abre um navegador, preenche o formulário, submete, etc...;

## Teste de cargas

- Relacionado a perfomance;
- A aplicação funciona com 1 usuário? Com 10? Com 100? 1000?
- Ajuda a descobrir gargalos;

# Boas práticas

## AAA - Arrange, Act e Assert

Prática que separa as etapas do teste da seguinte forma:

Arrange - Preparação, como criação dos mocks, instância, etc.

Act - Ação. Realização do testes;

Assert - Validação do teste.