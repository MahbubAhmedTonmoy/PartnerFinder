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

import { AuthService } from './service/auth.service'
import { HomeComponent } from './home/home.component';
import { RegistrationComponent } from './registration/registration.component';
import { ErrorInterceptorProvider } from './service/error.interceptor';
import { AlertifyService } from './service/alertify.service';
import { from } from 'rxjs';
import { MembersComponent } from './members/members.component';
import { LikesuserComponent } from './likesuser/likesuser.component';
import { MessagesComponent } from './messages/messages.component';
import { appRoutes } from './routes';
import { AuthGuard } from './guards/auth.guard';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegistrationComponent,
      MembersComponent,
      LikesuserComponent,
      MessagesComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      NgbModule,
      FormsModule, //templatefrom
      ReactiveFormsModule,
      RouterModule.forRoot(appRoutes),
      BsDropdownModule.forRoot()
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
