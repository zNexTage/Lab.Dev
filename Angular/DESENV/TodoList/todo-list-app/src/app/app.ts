import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import TodoItem from './app.type';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('todo-list-app');
  protected todoList: Array<TodoItem> = new Array();

  onSubmit(event: Event) : void{
    event.preventDefault();
    
    const formData = new FormData(event.target as HTMLFormElement);

    const title = formData.get("Title");
    const description = formData.get("Description");

    if(!title) {
      alert("Title is required");
      return;
    }

    if(!description) {
      alert("Description is required");
      return;
    }

    const todoItem = new TodoItem(title.toString(), description.toString());

    this.todoList = [...this.todoList, todoItem];
  }
}
