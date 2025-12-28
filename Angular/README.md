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
    

## Componentes

Um componente possui um classe Typescript, um template em HTML e estilização em CSS.

- O comportamento do componente é definido na classe typescript;


No Angular, todo componente possui a seguinte declaração:
```
import { Component } from '@angular/core';

@Component({
  selector: 'app-componente-teste',
  imports: [],
  templateUrl: './componente-teste.html',
  styleUrl: './componente-teste.css',
})
export class ComponenteTeste {

}
```

Pode-se utilizar `template` no lugar do `templateUrl` e `styles` no lugar de `styleUrl`. Exemplo:
```
import { Component, signal } from '@angular/core';

@Component({
  selector: 'app-root',
  //templateUrl: './app.html',
  template: `Hello Universe`,
  styles: `
    :host {
      color: blue;
    }
  `,
})
export class App {
  protected readonly title = signal('LearnAngular');
}

```

### Criando componente

Rodar o comando: `ng generate component ComponenteTeste`;
- Versão abreviada: `ng g c NomeComponente`


### Utilizando variáveis dentro de um componente

Declarando e utilizando variável (variável city):
```
import { Component, signal } from '@angular/core';

@Component({
  selector: 'app-root',
  //templateUrl: './app.html',
  template: `Hello {{city}}`, // Usando a variável aqui
  styles: `
    :host {
      color: #a144eb;
    }
  `,
})
export class App {
  protected readonly title = signal('LearnAngular');
  city:string = "São Paulo"; // Declarando a variável aqui
}
```

### Selector

O `selector` passado no decorator nos permite especificar como o componente será invocado.

Ex:

```
@Component({
  selector: 'app-user',
  template: ` Username: {{ username }} `,
})
export class User {
  username = 'Private Ryan';
}
```

O componente acima deve ser chamado da seguinte forma:
```
<app-user />
```

Obs: sempre que chamar um componente em outro componente é preciso coloca-lo na propriedade `imports`:

```
import { Component, signal } from '@angular/core';

@Component({
  selector: 'app-user',
  template: ` Username: {{ username }} `,
})
export class User {
  username = 'Private Ryan';
}

@Component({
  selector: 'app-root',
  imports: [User], // Importando o componente User para poder utilizar <app-user />
  //templateUrl: './app.html',
  template: `
  Hello {{city}}, {{currentDate}}
  <br />
  <app-user />
  `,
  styles: `
    :host {
      color: #a144eb;
    }
  `,
})
export class App {
  protected readonly title = signal('LearnAngular');
  city:string = "São Paulo";
  currentDate:string = new Date().toDateString();
}
```

### Condicionais

Utiliza-se o `@if`. Exemplo dentro do template:


```
 template: ` 
    @if(isLoggedIn){
      <p>
        Username: {{ username }}
      </p>
    } @else {
      <p>
        Welcome back, friend!
      </p>
    }  
  `,
```

O `@` é um sintaxe especial do Angular.

## Módulo

Comando: `ng g module Funcionalidade`

### Estrutura:

```
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    // Seus componentes
  ],
  imports: [
    // Seus módulos
    CommonModule
  ],
  providers: [
    // seus serviços
  ]
})
export class FuncionalidadeModule { }
```

## Services

Comando: `ng g service Servico`

**Parei em: https://angular.dev/tutorials/learn-angular/4-control-flow-if**