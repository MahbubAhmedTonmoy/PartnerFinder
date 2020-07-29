import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/service/user.service';
import { AlertifyService } from 'src/app/service/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery-9';

@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.css']
})
export class MemberDetailsComponent implements OnInit {
  
  galleryOptions : NgxGalleryOptions[];
  galleryImages : NgxGalleryImage[];
  user: User
  constructor(private userService: UserService,
    private alert: AlertifyService,
    private route: ActivatedRoute) { } // route for pass is in url

  ngOnInit() {
    this.loadUser();

    //photo gallery

    this.galleryOptions = [
      {
          width: '600px',
          height: '400px',
          thumbnailsColumns: 4,
          imageAnimation: NgxGalleryAnimation.Slide,
          preview: false
      },
      // max-width 800
      {
          breakpoint: 800,
          width: '100%',
          height: '600px',
          imagePercent: 80,
          thumbnailsPercent: 20,
          thumbnailsMargin: 20,
          thumbnailMargin: 20
      },
      // max-width 400
      {
          breakpoint: 400,
          preview: false
      }
    ];

    this.galleryImages = this.getImages();
  }

  loadUser(){
    this.userService.getUser(this.route.snapshot.params['id']).subscribe((user: User) =>{
      this.user = user;
      console.log(this.user);
    }, error => {
      this.alert.error(error);
    })
  }

  getImages() {
    const imageURLs = [];
    for(let i=0; i< this.user.photo.length; i++){
      imageURLs.push({
        small: this.user.photo[i].url,
        medium: this.user.photo[i].url,
        big: this.user.photo[i].url,
        description: this.user.photo[i].description
      });
    }
    return imageURLs;
  }

}
