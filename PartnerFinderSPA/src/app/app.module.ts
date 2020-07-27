import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';
import { AuthService } from './service/auth.service'
import { HomeComponent } from './home/home.component';
import { RegistrationComponent } from './registration/registration.component';
import { ErrorInterceptorProvider } from './service/error.interceptor';
import { AlertifyService } from './service/alertify.service';
import { from } from 'rxjs';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegistrationComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      NgbModule,
      FormsModule,//templatefrom
      ReactiveFormsModule
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
