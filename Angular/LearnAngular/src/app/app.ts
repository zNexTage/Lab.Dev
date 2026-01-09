import { Component, input, signal } from '@angular/core';
import { Child } from './child/child';
import { NgOptimizedImage } from '@angular/common';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-home',
   imports: [Child],  
  template: `
   <span>
    Hello {{city}}, {{currentDate}}
  </span>
  <br />
  <!-- <app-user [occupation]="occupation" /> -->

  <!-- <button (click)="changeOccupation('Angular Developer')">
      Change occupation to Angular Developer
  </button> -->

  <ul>
  @for(os of operatingSystens; track os.id){
    <li>{{os.name}}</li>
  }
  </ul>

  <div [contentEditable]="isDivEditable"></div>

  <button (click)="greet()">
    Clique aqui!
  </button>

    <section (mouseover)="showSecretMessage()" (mouseout)="hideSecretMessage()">
      There's a secret message for you, hover to reveal 👀
      {{ message }}
    </section>

    <app-child (incrementCountEvent)="onCount($event)" />

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
  styles: `
    :host {
      color: #a144eb;
    }
  
  `
})
export class Home {
protected readonly title = signal('LearnAngular');
  city: string = "São Paulo";
  currentDate: string = new Date().toDateString();
  operatingSystens: Array<{ id: string, name: string }> = [
    {
      id: "win",
      name: "Windows"
    },
    {
      id: "osx",
      name: "MacOS"
    },
    {
      id: "linux",
      name: "Linux"
    },
  ]
  isDivEditable: boolean = false;

  message:string = "";
  //occupation:string ="Soldier";

  greet() {
    alert("Hello, there!");
  }

  showSecretMessage(){
    this.message = "oloko";
  }

  hideSecretMessage(){
    this.message = "";
  }

  // changeOccupation(newOccupation:string){
  //   this.occupation = newOccupation;
  // }

  onCount(count:number){
    console.log(count);
  }
}

@Component({
  selector: 'app-user',
  imports: [NgOptimizedImage],
  template: ` 
    @if(isLoggedIn){
      <img ngSrc="https://ew.com/thmb/hXirYe4etmhfJbmU5dL9d0tXF3c=/2000x0/filters:no_upscale():max_bytes(150000):strip_icc()/saving-private-ryan-matt-damon-060524-1-27570d4206364916891172e478add38c.jpg" alt="Angular logo" width="250" height="250" />
      <p>
        Username: {{ username }} - Occupation: {{occupation()}}
      </p>      
    } @else {
      <p>
        Welcome back, friend!
      </p>
    }
  
  `,
})
export class User {
  username = 'Private Ryan';
  isLoggedIn = true;
  occupation = input<string>();
}

@Component({
  selector: 'app-root',
  imports: [RouterLink, RouterOutlet],
  //templateUrl: './app.html',
  template: `
   <nav>
      <a routerLink="/user">Usuários</a> | 
      <a routerLink="/">Home</a>
    </nav>
     <router-outlet />
  `,
})
export class App {
  
}
