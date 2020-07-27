import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MembersComponent } from './members/members.component';
import { MessagesComponent } from './messages/messages.component';
import { LikesuserComponent } from './likesuser/likesuser.component';
import { AuthGuard } from './guards/auth.guard';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'members', component: MembersComponent},
            { path: 'message', component: MessagesComponent},
            { path: 'lists', component: LikesuserComponent},
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
]