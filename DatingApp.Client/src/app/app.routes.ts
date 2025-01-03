import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { MemberListsComponent } from './features/members/member-lists/member-lists.component';
import { ListsComponent } from './features/lists/lists.component';
import { MessagesComponent } from './features/messages/messages.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'members', component: MemberListsComponent },
  { path: 'members/:id', component: MemberListsComponent },
  { path: 'lists', component: ListsComponent },
  { path: 'messages', component: MessagesComponent },
  { path: '**', component: HomeComponent, pathMatch: 'full' },
];
