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

**Idealmente, podemos utilizar o componente raíz para configurar uma navbar para realizar navegação.**
**Ex:**
```
import {Component} from '@angular/core';
import {RouterOutlet} from '@angular/router';

@Component({
  selector: 'app-root',
  template: `
    <nav>
      <a href="/">Home</a>
      |
      <a href="/user">User</a>
    </nav>
    <router-outlet />
  `,
  imports: [RouterOutlet],
})
export class App {}
```

Obs: toda vez que for clicado em um link, ele será renderizado no lugar de `<router-outlet />`.


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

### Iteração

Utiliza-se o `@for`. Exemplo:
```
@Component({
  ...
  template: `
    @for (os of operatingSystems; track os.id) {
      {{ os.name }}
    }
  `,
})
export class App {
  operatingSystems = [{id: 'win', name: 'Windows'}, {id: 'osx', name: 'MacOS'}, {id: 'linux', name: 'Linux'}];
}
```




### Binding de propriedades

Para permitir que uma propriedade seja dinâmica, utilize a seguinte sintaxe:

`<div [contentEditable]="false"></div>`

Basta colocar colchetes na propriedade, conforme mostrado no exemplo acima. Fazendo isso, é possível fazer o seguinte:

```
import {Component} from '@angular/core';

@Component({
  selector: 'app-root',
  styleUrls: ['app.css'],
  template: ` <div [contentEditable]="isEditable"></div> `,
})
export class App {
  isEditable = true;
}
```

Agora o `contentEditable` define o seu valor conforme a variável `isEditable`.

### Eventos

É possível atribuir funções para eventos através da seguinte sintaxe:

```
 <button (click)="greet()">
    Clique aqui!
  </button>

```

Coloque entre parênteses o evento. Exemplo: `(click)`. **Se atentar que o nome dos eventos são um pouco diferentes do usados diretamente no HTML.**

### Passando parâmetros para componentes (input)

No Angular, a passagem de parâmetros para componentes é feito usando input (similar ao props).

Exemplo:

Declarando
```
@Component({
  selector: 'app-user',
  template: ` 
    Occupation: {{occupation()}}  
  `,
})
class User {  
  occupation = input<string>();
}
```

Passando parâmetro:

`@Component({  
  ...  
  template: `<app-user occupation="Angular Developer"></app-user>`
})
export class App {}
 `

 Podemos colocar o input dentro de colchetes se quisermos passar valores de variáveis.

### Recebendo valores de um componente filho

Para passar para um componente pai o retorno de um componente filho deve-se fazer o seguinte:

- Criar um propriedade que recebe o retorno de `output<T>()`. Dessa forma: `incrementCountEvent = output<number>();`;
  - Esse exemplo está dizendo que há um evento chamado incrementCountEvent que passa um número como parâmetro;

Estrutura completa:

```
import { Component, output } from '@angular/core';

@Component({
  selector: 'app-child',
  imports: [],
  templateUrl: './child.html',
  styleUrl: './child.css',
})
export class Child {
  incrementCountEvent = output<number>();
  count = 0;

  onClick(){
    this.count ++;
    this.incrementCountEvent.emit(this.count); //Passa o valor do contador como parâmetro.
  }
}
```
Na classe pai basta fazer a seguinte invocação:

```
<app-child (incrementCountEvent)="onCount($event)" />
```

- `onCount` é uma função criada no componente pai e que recebe um número como parâmetro;
- Todas as vezes que o `emit` de `incrementCountEvent` for invocado, `onCount` será chamado e receberá o valor de `count` como parâmetro;


### Template

Todo componente utiliza um template que consiste em código HTML. 

#### Controle do carregamento e da renderização condicional do template

É possível controlar quando partes do template são carregadas e renderizadas por meio dos blocos `@defer`, `@loading` e `@placeholder`.

`@defer` -> define um bloco de conteúdo cuja carga e renderização são adiadas, ocorrendo somente quando uma condição ou gatilho é satisfeito (por exemplo, `on viewport`, `on interaction` ou `when`). Os gatilhos do `@defer` são avaliados durante a renderização do template do componente pai e determinam quando o conteúdo deferido será carregado e inserido no DOM.
  - `on viewport` -> será carregado quando ele entrar na área visível da tela. Se estiver na parte inferior da tela, só é carregado quando o usuário scrola até a região do bloco (visibilidade). Só é invocado se a janela de visualização estiver na região que o bloco @defer foi declarado, isto é, visível.
      - **`on viewport` observa o local do @defer, não o ciclo de vida do componente pai.**;

  - `on interaction` -> será carregado após uma interação do usuário;
    - Normalmente, a interação é feita através de um elemento declarado no bloco `@placeholder`;
    - Interações disponíveis: `click`, `focus`, `keydown`, `pointerdown` e `touchstart` (mouse, teclado e toque);
      - Ex:
        ```
        @defer (on interaction) {
          <app-comments />
        } @placeholder {
          <button>Mostrar comentários</button>
        }
        ```
     

  - `when` -> será carregado quando uma expressão booleana se tornar verdadeira.
Exemplos:
```
@defer (on viewport) {
  <app-chart />
}

@defer (on interaction) {
  <app-comments />
}

@defer (when isLoggedIn) {
  <app-dashboard />
}

@defer (on viewport; when isReady) {
  <app-heavy />
}
```


`@placeholder` -> define um conteúdo leve que é renderizado imediatamente, enquanto o conteúdo do @defer ainda não foi carregado.

`@loading` -> define um conteúdo exibido durante o carregamento do conteúdo deferido, caso esse carregamento não seja imediato.

Exemplo de uso:

```
@Component({
  selector: 'app-root',
  template: `
    @defer {
      <p>This is deferred content!</p>
    }
    @placeholder {
      <p>This is placeholder content!</p>
    }
    @loading (minimum 2s) {
      <p>Loading</p>
    }
  `,
})
```

# Otimização de imagem

- Deve-se utilizar o `NgOptimizedImage`;
  `import { NgOptimizedImage } from '@angular/common';`
- Adicionar como import no @Component:
  ```
  @Component({  imports: [NgOptimizedImage],  ...})
  ```
- Então, agora deve-se utilizar a propriedade `ngSrc` no elemento `img`:
  - `<img ngSrc="/assets/logo.svg" alt="Angular logo" width="32" height="32" />`
- Obs: o `NgOptimizedImage` exige que seja informado a altura (height) e largura (width)
  - Se não for possível informar uma altura e largura, então adicione a propriedade `fill`:
   ```
   <div class="image-container"> //Container div has 'position: "relative"'  <img ngSrc="www.example.com/image.png" fill /></div>
   ```
   Obs: Para o `fill` funcionar, o elemento pai deve ter o `position` definido como `relative`, `fixed` ou `absolute`;

## Prioridade
`<img ngSrc="www.example.com/image.png" height="600" width="800" priority />`
- Indica que a imagem é crítica e deve ser carregada o mais cedo possível;
- A imagem começa a ser baixada antes mesmo de chegar ao img;
  - `<link rel="preload" as="image">` -> esse código é injetado no `<head>`;

## Image loader

É possível definir um `image loader` para especificar a URL base e, dessa forma, colocar apenas o nome da imagem no atributo `ngSrc`;
`providers: [provideImgixLoader('https://my.base.url/')];` -> colocar no @Component;
`<img ngSrc="image.png" height="600" width="800" />` -> agora não precisa colocar a URL completa.



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

## Rotas

Para configurar rotas é necessário acessar o arquivo `app.rotes.ts` e estruturar conforme exemplo abaixo:

Arquivo: `app.rotes.ts`
```
export const routes: Routes = [
    {
        path: '',
        title: "Home",
        component: App
    },
    {
        path: 'user',
        title: "Usuários",
        component: User
    }
];
```

# Formulários

Angular trabalha com o formato `template-driven` e `reativo`.


## template-driven
É necessário importar o módulo `FormsModule` e acrescenta-lo no componente (no imports do decorator da classe):
`import {FormsModule} from '@angular/forms';` 

```
<label for="framework">
    Favorite Framework:
    <input id="framework" type="text"  [(ngModel)]="favoriteFramework"/>
</label>
```
`favoriteFramework` deve ser um atributo criado dentro da classe do template. Todo valor digitado no campo será armazenado no atributo `favoriteFramework`.

`[()]` -> essa sintaxe é usada para binding de eventos e dados.

## Reativo
É necessário importar o módulo `ReactiveFormsModule` e acrescenta-lo no componente (no imports do decorator da classe):
`import { ReactiveFormsModule } from '@angular/forms';`

O modelo reativo trabalha com `FormGroup` que agrupa `FormControls`. Podemos dizer que se trata de um agrupamento de controles (inputs).

```
import {ReactiveFormsModule, FormControl, FormGroup } from '@angular/forms';
...
export class App {
  profileForm = new FormGroup({
    name: new FormControl(''),
    email: new FormControl(''),
  });
}
```

Cada `FormGroup` deve estar vinculado a um formulário através da propriedade `[formControl]`.
`<form [formGroup]="profileForm">`

Cada campo no formulário deve apontar para um atributo do `FormGroup`, e isso é feito através da propriedade `formControlName`.
`<input type="text" formControlName="name" />`

Os valores são acessados da seguinte maneira:
`<p>Name: {{ profileForm.value.name }}</p>`.

Para capturar o evento de submissão, basta utilizar a propriedade `(ngSubmit)` na tag `<form>`
```
<form [formGroup]="profileForm" (ngSubmit)="handleSubmit()">
```

```
handleSubmit() {
  alert(
    this.profileForm.value.name + ' | ' + this.profileForm.value.email
  );
}
```

## Validando formulários

Importe o módulo de validação:
`import {ReactiveFormsModule, Validators} from '@angular/forms';`

O `Validators` pode ser usado para validar campo no formulário. Exemplo:
```
profileForm = new FormGroup({
  name: new FormControl('', Validators.required),
  email: new FormControl('', [Validators.required, Validators.email]),
});
```
No exemplo acima é definido que `name` será obrigatório; o campo `email` deve estar formatado corretamente e não pode ser nulo.

Para saber se o formulário está valido, basta acessar a propriedade `valid` da instância do `FormGroup`.
`<button type="submit" [disabled]="!profileForm.valid">Submit</button>`

## Services

Comando: `ng g service Servico`

**Parei em: https://angular.dev/tutorials/learn-angular/10-deferrable-views**