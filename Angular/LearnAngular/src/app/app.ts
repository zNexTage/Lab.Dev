import { Component, signal } from '@angular/core';

@Component({
  selector: 'app-user',
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
})
export class User {
  username = 'Private Ryan';
  isLoggedIn = true;
}

@Component({
  selector: 'app-root',
  imports: [User],
  //templateUrl: './app.html',
  template: `
  <span>
    Hello {{city}}, {{currentDate}}
  </span>
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
