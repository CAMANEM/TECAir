import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { Vuelo } from '../models/vuelo.module';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VuelosService {
  baseApiUrl: string = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  postVuelo(vuelo: Vuelo): Observable<number> {
    return this.http.post<number>(this.baseApiUrl + '/api/Vuelos', vuelo);
  }
}