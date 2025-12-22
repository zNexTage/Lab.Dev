# Angular

## O que é SPA?

- Single Page Application;
- Toda página SPA trabalha com requisição assincrona, buscando os dados conforme a necessidade sem precisar carregar a tela inteira;

## Um novo Angular

### O que é Angular?

- **Framework** web, mobile e desktop;
    - Podemos trabalhar com PWA;

#### AngularJS e Angular

- **Não são a mesma coisa;**
- 2010 - Nascimento do AngularJS - 1.x
    - Framework MV* (MVC e MVVM)
    - Ling Javascript
- 2015 - Angular 2 **Just Angular**
    - Baseado em WebComponents;
    - Ling Typescript
    - Não era compatível com o AngularJS.
- 2016 - Angular 4
    - Pulou do 2 para o 4;
- 2017 - Angular 5;
- 2018 - Angular 6 e 7;
- 2019 - Angular 8 e 9;
- 2020 - Angular 10 e 11;

As versões estão saindo a cada 6 meses.

## Versionamento

- O Angular utiliza Semantic Versioning;
    - 2.3.1
        - 2: major - breaking changes;
            - Pode haver quebra da versão anterior.
        - 3: minor - new features, not breaking;
            - Ganho de novas funcionalidades e elas não quebram a versão.
        - 1: patch - bugfixes, not breaking.

| Releases | Frequência |
|----------|------------|
| Major "1.x.x" | A cada 6 meses |
| Minor "x.1.x" | De 1 a 3 a cada major release |
| Patch "x.x.1" | Quase todas as semanas |

| Previews | |
| ---------|--|
| Beta | Em fase de desenvolvimento e testes. É identificável como por ex: "10.0.0-beta.0". |
| Release Candidate | Desenvolvimento completo e em testes finais. É identificável como por ex: "10.1.10-rc". | 

## Anatomia de uma App Angular

- Trabalha com componentes;
    Uma aplicação é uma composição de componentes.
- Cria-se serviços (services) para se comunicar com o mundo externo;+

### Angular é baseado em componentes

Um componente é um Template (HTML). Um template possui uma classe e a classe possui Metadados.

Componente = Template + Class + Metadado.

## Configurando o ambiente

- Precisa ter instalado o Node e o NPM;
- Instalar o Angular CLI;
    - `npm install -g @angular/cli`

