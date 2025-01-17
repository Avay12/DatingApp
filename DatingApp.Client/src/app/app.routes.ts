import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { MemberListComponent } from './features/members/member-list/member-list.component';
import { ListsComponent } from './features/lists/lists.component';
import { MessagesComponent } from './features/messages/messages.component';
import { authGuard } from './core/guard/auth.guard';
import { TestErrorsComponent } from './features/errors/test-errors/test-errors.component';
import { NotFoundComponent } from './features/errors/not-found/not-found.component';
import { ServerErrorsComponent } from './features/errors/server-errors/server-errors.component';
import { MemberDetailComponent } from './features/members/member-detail/member-detail.component';
import { MemberEditComponent } from './features/members/member-edit/member-edit.component';
import { preventUnsavedChangesGuardGuard } from './core/guard/prevent-unsaved-changes-guard.guard';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      {
        path: 'members',
        component: MemberListComponent,
      },
      { path: 'members/:username', component: MemberDetailComponent },
      {
        path: 'member/edit',
        component: MemberEditComponent,
        canDeactivate: [preventUnsavedChangesGuardGuard],
      },
      { path: 'lists', component: ListsComponent },
      { path: 'messages', component: MessagesComponent },
    ],
  },
  { path: 'errors', component: TestErrorsComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorsComponent },
  { path: '**', component: HomeComponent, pathMatch: 'full' },
];
