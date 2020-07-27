import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../service/auth.service';
import { AlertifyService } from '../service/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router,
	private alertify: AlertifyService) {} // create authservice login(),register(),loggedin() method here, alertify service

	canActivate(): boolean {
	if (this.authService.loggedIn()) {
		return true; // route active if true
	}

	this.alertify.error('please login first');
	this.router.navigate(['/home']);
	return false;
	}
}