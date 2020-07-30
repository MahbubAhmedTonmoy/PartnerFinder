import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/service/auth.service';
import { UserService } from 'src/app/service/user.service';
import { AlertifyService } from 'src/app/service/alertify.service';


//child of members 
@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {

  @Input() user: User; // receive from parent 
  
  constructor(private authService: AuthService,
    private userService: UserService,
    private alert: AlertifyService) { }

  ngOnInit() {
  }

  hitLike(receiverId: string){
    this.userService.sendLike(this.authService.decodedToken.nameid, receiverId)
      .subscribe(data => {
        console.log(data);
        this.alert.message("send like");
      }, error => {
        this.alert.error(error);
      })
  }

}
