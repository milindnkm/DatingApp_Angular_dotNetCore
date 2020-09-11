import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from 'ngx-gallery';
import { error } from 'protractor';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/Alertify.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.scss']
})
export class MemberDetailsComponent implements OnInit {
  user: User;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor(private userService: UserService, 
    private alertify: AlertifyService,
    private routes: ActivatedRoute) { }

  ngOnInit() {
    this.routes.data.subscribe(data => {
      this.user = data['user'];
    });

    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ];
    this.galleryImages = this.getImage();
  }

  getImage() {
    const imageUrls = [];

    for (const photo of this.user.photos) {
      imageUrls.push( {
        small: photo.url,
        medium: photo.url,
        big: photo.url,
        description: photo.description
      });
    }
    console.log(imageUrls);
    return imageUrls;
  }    
  
  // + will convert it to Int
  // loadUser() {
  //   this.userService.getUser(+this.routes.snapshot.params['id']).subscribe((user: User) => {
  //     this.user = user;
  //   }, error => {
  //     this.alertify.error(error);
  //   });
  // }
}
