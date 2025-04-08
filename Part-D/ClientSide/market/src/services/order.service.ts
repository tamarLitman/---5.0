import { Injectable } from '@angular/core';
import { Order } from '../classes/Order';
import { Observable } from 'rxjs';
import { environment } from '../environment';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class OrderService {

  AllOrders:Array<Order>=new Array<Order>;
  constructor(public http:HttpClient) { }
  getAll():Observable<Array<Order>>
  {
    console.log(environment.apiUrl+"/Order");
    return this.http.get<Array<Order>>(environment.apiUrl+"/Order")
  }
  addOrder(O: Order):Observable<Order>
  {
    return this.http.post<Order>(environment.apiUrl+"/Order",O)
  }
  getByCode(code:number):Observable<Order>
  {
    return this.http.get<Order>(environment.apiUrl+"/Order/"+code)
  }
  update(O:Order):Observable<Boolean>{
    return this.http.put<Boolean>(environment.apiUrl+"/Order",O)
  }
}

