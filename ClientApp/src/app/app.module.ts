import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { EditGundamComponent } from './gundam/edit-gundam/edit-gundam.component';
import { FetchGundamComponent } from './gundam/fetch-gundam/fetch-gundam.component';
import { FetchBacklogComponent } from './backlog/fetch-backlog/fetch-backlog.component';
import { EditBacklogComponent } from './backlog/edit-backlog/edit-backlog.component';
import { FetchWishlistComponent } from './wishlist/fetch-wishlist/fetch-wishlist.component';
import { EditWishlistComponent } from './wishlist/edit-wishlist/edit-wishlist.component';
import { FetchSaleComponent } from './sale/fetch-sale/fetch-sale.component';
import { FetchUserComponent } from './user/fetch-user/fetch-user.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FetchGundamComponent,
    EditGundamComponent,
    FetchBacklogComponent,
    EditBacklogComponent,
    FetchWishlistComponent,
    EditWishlistComponent,
    FetchSaleComponent,
    FetchUserComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'fetch-gundam', component: FetchGundamComponent, canActivate: [AuthorizeGuard] },
      { path: 'fetch-backlog', component: FetchBacklogComponent, canActivate: [AuthorizeGuard] },
      { path: 'fetch-wishlist', component: FetchWishlistComponent, canActivate: [AuthorizeGuard] },
      { path: 'fetch-sale', component: FetchSaleComponent },
      { path: 'fetch-user', component: FetchUserComponent },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
