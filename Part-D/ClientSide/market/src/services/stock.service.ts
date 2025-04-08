import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environment';
import { HttpClient } from '@angular/common/http';
import { Stock } from '../classes/Stock';
@Injectable({
  providedIn: 'root'
})
export class StockService {

  AllProd:Array<Stock>=new Array<Stock>;
  constructor(public http:HttpClient) { }
  getAll():Observable<Array<Stock>>
  {
    return this.http.get<Array<Stock>>(environment.apiUrl+"/Stock")
  }
  addStock(S: Stock):Observable<Stock>
  {
    return this.http.post<Stock>(environment.apiUrl+"/Stock",S)
  }
  getByCode(code:number):Observable<Stock>
  {
    return this.http.get<Stock>(environment.apiUrl+"/Stock/"+code)
  }
}

