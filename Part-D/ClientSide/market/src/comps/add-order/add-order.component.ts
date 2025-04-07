import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { OrderService } from '../../services/order.service';
import { Router } from '@angular/router';
import { SupplierService } from '../../services/supplier.service';
import { StockService } from '../../services/stock.service';
import { Stock } from '../../classes/Stock';
import { Order } from '../../classes/Order';
import { MatCardModule } from '@angular/material/card';
import { log } from 'console';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-order',
  standalone: true,
  providers: [OrderService,StockService],
  imports: [MatCardModule, MatButtonModule,HttpClientModule,HttpClientModule,CommonModule],
  templateUrl: './add-order.component.html',
  styleUrl: './add-order.component.css'
})
export class AddOrderComponent implements OnInit{
  newOrder:Order={
    orderId: 0,
    orderStateId: 1,
    state: "Done",
    prods: new Array<Stock>
  }
  constructor(public router:Router,public stockService:StockService,public orderService:OrderService) {
    
  }
  ngOnInit(): void {
    this.stockService.getAll()
    .subscribe(
      succ=>{
        this.stockService.AllProd=succ;
      },
      err=>{
        console.log('gat-all-stock-faild');
        console.log(err);
        
      }
    )
  }
  addProdToOrder(stock:Stock){
      this.newOrder?.prods?.push(stock);
  }
  completeOrder(){
    debugger
    this.orderService.addOrder(this.newOrder!)
    .subscribe(
      succ=>{
        console.log(succ);
      },
      err=>{
        console.log(err);
      }
    )
  }

}
