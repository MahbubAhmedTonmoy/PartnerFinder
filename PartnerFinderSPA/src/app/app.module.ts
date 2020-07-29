import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { JwtModule } from "@auth0/angular-jwt";
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxGalleryModule } from 'ngx-gallery-9';

import { AuthService } from './service/auth.service'
import { HomeComponent } from './home/home.component';
import { RegistrationComponent } from './registration/registration.component';
import { ErrorInterceptorProvider } from './service/error.interceptor';
import { AlertifyService } from './service/alertify.service';
import { UserService } from './service/user.service';
import { from } from 'rxjs';
import { MembersComponent } from './memberslist/members/members.component';
import { MemberCardComponent } from './memberslist/member-card/member-card.component';
import { MemberDetailsComponent } from './memberslist/member-details/member-details.component';
import { LikesuserComponent } from './likesuser/likesuser.component';
import { MessagesComponent } from './messages/messages.component';
import { appRoutes } from './routes';
import { AuthGuard } from './guards/auth.guard';

export function tokenGetter() {
   return localStorage.getItem('token');
 }

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegistrationComponent,
      MembersComponent,
      LikesuserComponent,
      MessagesComponent,
      MemberCardComponent,
      MemberDetailsComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      NgbModule,
      FormsModule, //templatefrom
      ReactiveFormsModule,
      RouterModule.forRoot(appRoutes),
      BsDropdownModule.forRoot(),
      TabsModule.forRoot(),
      NgxGalleryModule,
		JwtModule.forRoot({
         config: {
           tokenGetter: tokenGetter,
           allowedDomains: ['localhost:44322'],
           disallowedRoutes: ['localhost:44322/api/auth']
         }
       })
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard,
      UserService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
