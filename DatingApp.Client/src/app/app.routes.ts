import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { MemberListsComponent } from './features/members/member-lists/member-lists.component';
import { ListsComponent } from './features/lists/lists.component';
import { MessagesComponent } from './features/messages/messages.component';
import { authGuard } from './core/guard/auth.guard';
import { TestErrorsComponent } from './features/errors/test-errors/test-errors.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      {
        path: 'members',
        component: MemberListsComponent,
      },
      { path: 'members/:id', component: MemberListsComponent },
      { path: 'lists', component: ListsComponent },
      { path: 'messages', component: MessagesComponent },
    ],
  },
  { path: 'errors', component: TestErrorsComponent },
  { path: '**', component: HomeComponent, pathMatch: 'full' },
];
