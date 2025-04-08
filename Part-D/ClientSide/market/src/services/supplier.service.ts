import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Supplier } from '../classes/Supplier';
import { Observable } from 'rxjs';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class SupplierService {
    AllSuppliers:Array<Supplier>=new Array<Supplier>;
  
    constructor(public http:HttpClient) { }
    getAll():Observable<Array<Supplier>>
    {
      return this.http.get<Array<Supplier>>(environment.apiUrl+"/Supplier")
    }
    addSupplier(newSupplier: Supplier):Observable<Supplier>
    {
      return this.http.post<Supplier>(environment.apiUrl+"/Supplier",newSupplier)
    }
    getByCode(code:number):Observable<Supplier>
    {
      return this.http.get<Supplier>(environment.apiUrl+"/Supplier/"+code)
    }
}
