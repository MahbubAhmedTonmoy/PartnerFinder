import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MembersComponent } from './memberslist/members/members.component';
import { MessagesComponent } from './messages/messages.component';
import { LikesuserComponent } from './likesuser/likesuser.component';
import { AuthGuard } from './guards/auth.guard';
import { MemberDetailsComponent } from './memberslist/member-details/member-details.component';
import { MemberEditComponent } from './memberslist/member-edit/member-edit.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'members', component: MembersComponent},
            { path: 'members/:id', component: MemberDetailsComponent},
            { path: 'members/edit/:id', component: MemberEditComponent},
            { path: 'message', component: MessagesComponent},
            { path: 'lists', component: LikesuserComponent},
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
]