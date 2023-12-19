import { Component } from '@angular/core';

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
  gundams: Gundam[] = [];
  backlogs: Backlog[] = [];
  wishlist: WishList[] = [];

  constructor(private gundamService: GundamService, private backlogService: BacklogService, private wishlistService: WishlistService) {}

  search() {
    this.gundamService.getUserGundam(this.userName).subscribe((result: Gundam[]) => (this.gundams = result));
    this.backlogService.getUserBacklog(this.userName).subscribe((result: Backlog[]) => (this.backlogs = result));
    this.wishlistService.getUserWishList(this.userName).subscribe((result: Gundam[]) => (this.wishlist = result));
  }

  favoriteUsers: string[] = [];

  addToFavorites() {
    if (this.userName && !this.favoriteUsers.includes(this.userName)) {
      this.favoriteUsers.push(this.userName);
    }
  }

  removeFromFavorites(user: string) {
    const index = this.favoriteUsers.indexOf(user);
    if (index !== -1) {
      this.favoriteUsers.splice(index, 1);
    }
  }
}
