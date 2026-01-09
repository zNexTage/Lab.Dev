import { Routes } from '@angular/router';
import { Home, User } from './app';

export const routes: Routes = [
    {
        path: '',
        title: "Home",
        component: Home
    },
    {
        path: 'user',
        title: "Usuários",
        component: User        
    }
];
