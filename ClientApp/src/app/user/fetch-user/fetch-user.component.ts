import { Component } from '@angular/core';

import { FavUser } from 'src/app/_models/favUser';
import { FavUserService } from 'src/app/_services/fav-user.service';

import { Gundam } from 'src/app/_models/gundam';
import { Backlog } from 'src/app/_models/backlog';
import { WishList } from 'src/app/_models/wishList';

import { GundamService } from 'src/app/_services/gundam.service';
import { BacklogService } from 'src/app/_services/backlog.service';
import { WishlistService } from 'src/app/_services/wishlist.service';

@Component({
  selector: 'app-fetch-user',
  templateUrl: './fetch-user.component.html'
})
export class FetchUserComponent {

  userName: string = "";
  selectedFavUserName: string = "";
  favUsers: FavUser[] = [];
  gundams: Gundam[] = [];
  backlogs: Backlog[] = [];
  wishlist: WishList[] = [];

  constructor(private gundamService: GundamService, private backlogService: BacklogService, private wishlistService: WishlistService, private favUserService: FavUserService) {}

  ngOnInit() : void {
    this.favUserService.getFavUser().subscribe((result: FavUser[]) => (this.favUsers = result));
  }

  searchSelected() {
    this.gundamService.getUserGundam(this.selectedFavUserName).subscribe((result: Gundam[]) => (this.gundams = result));
    this.backlogService.getUserBacklog(this.selectedFavUserName).subscribe((result: Backlog[]) => (this.backlogs = result));
    this.wishlistService.getUserWishList(this.selectedFavUserName).subscribe((result: WishList[]) => (this.wishlist = result));
  }

  search() {
    this.gundamService.getUserGundam(this.userName).subscribe((result: Gundam[]) => (this.gundams = result));
    this.backlogService.getUserBacklog(this.userName).subscribe((result: Backlog[]) => (this.backlogs = result));
    this.wishlistService.getUserWishList(this.userName).subscribe((result: WishList[]) => (this.wishlist = result));
  }

  selectFavUser(user: FavUser) {
    this.selectedFavUserName = user.userName;
  }

  createFavUser() {
    const newFavUser: FavUser = { userName: this.userName };
    this.favUserService.createFavUser(newFavUser).subscribe((result: FavUser[]) => {
      this.favUsers = result;
    });
    this.ngOnInit();
  }

  deleteFavUser() {
    const favUserToDelete = this.favUsers.find(user => user.userName === this.selectedFavUserName);
    if (favUserToDelete && window.confirm('Mobile Suit Pilot, do you truly wish to annihilate this target?')) {
      this.favUserService
        .deleteFavUser(favUserToDelete)
        .subscribe((favUsers: FavUser[]) => this.favUsers = favUsers);
    }
  }
}

