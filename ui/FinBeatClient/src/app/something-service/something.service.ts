import {Injectable} from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {SomethingPaged} from './dto/SomethingPaged';
import {Observable} from 'rxjs';
import {CreateSomethingDto} from './dto/CreateSomethingDto';

@Injectable({
  providedIn: 'root'
})
export class SomethingService {

  constructor(private readonly http: HttpClient) {
  }

  getSomethings(pageNumber: number, pageItemsCount: number): Observable<SomethingPaged> {
    const url = `${environment.baseUrl}/Something/GetPaged`;
    return this.http.get<SomethingPaged>(url, {
      params: {
        pageNumber: pageNumber,
        pageItemsCount: pageItemsCount
      }
    });
  }

  rewriteSomethings(somethings: CreateSomethingDto[]): Observable<any> {
    const url = `${environment.baseUrl}/Something/Rewrite`;
    return this.http.post(url, somethings);
  }
}
