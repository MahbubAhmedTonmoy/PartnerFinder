import { Component, OnInit } from '@angular/core';
import { UserForRegistration } from '../models/UserForRegistration';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
 
  userforRegInfo: UserForRegistration;
  registerForm: FormGroup;

  constructor(private authService: AuthService) { }

  ngOnInit() {

    this.registerForm = new FormGroup({
      role: new FormControl('User'),
      fullName: new FormControl('', Validators.required), 
      email: new FormControl('', Validators.required), 
      username: new FormControl('', Validators.required),      //['', Validators.required]
      password: new FormControl('', Validators.required),
      confirmPassword: new FormControl('', Validators.required)
    })
  }
  register(){
    if(this.registerForm.valid){
      this.userforRegInfo = Object.assign({}, this.registerForm.value); // this.registerForm.value clone ihe value in {} empty object ,then assign {} to this.user
      this.authService.register(this.userforRegInfo).subscribe(next => {
        console.log("loged in");
      }, error => {
        console.log('failed to log');
      });
    }}
}
