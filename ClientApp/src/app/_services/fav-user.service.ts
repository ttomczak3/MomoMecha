import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FavUser } from '../_models/favUser';

@Injectable({
  providedIn: 'root'
})
export class FavUserService {
  private url = "favoriteuser";
  constructor(private http: HttpClient) { }

  public getFavUser() : Observable<FavUser[]> {
    return this.http.get<FavUser[]>(`${environment.baseUrl}/${this.url}`);
  }
  public createFavUser(favUser: FavUser) : Observable<FavUser[]> {
    return this.http.post<FavUser[]>(`${environment.baseUrl}/${this.url}`, favUser);
  }
  public deleteFavUser(favUser: FavUser): Observable<FavUser[]> {
    return this.http.delete<FavUser[]>(
      `${environment.baseUrl}/${this.url}/${favUser.id}`
    );
  }
}
