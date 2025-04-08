import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { OrderService } from '../../services/order.service';
import { Router } from '@angular/router';
import { StockService } from '../../services/stock.service';
import { Stock } from '../../classes/Stock';
import { Order } from '../../classes/Order';
import { MatCardModule } from '@angular/material/card';

import { CommonModule } from '@angular/common';
import { SupplierService } from '../../services/supplier.service';
import { Supplier } from '../../classes/Supplier';
import { log } from 'console';

@Component({
  selector: 'app-add-order',
  standalone: true,
  providers: [OrderService,StockService,SupplierService],
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
  displayStock=false;
  constructor(public router:Router,public stockService:StockService,public orderService:OrderService,public supplierService:SupplierService) {
    
  }
  ngOnInit(): void {
    this.supplierService.getAll()
    .subscribe(
      succ=>{
        debugger
        console.log(succ);
        this.supplierService.AllSuppliers=succ;
        console.log(this.supplierService.AllSuppliers.at(0)?.companyName);
        
      },
      err=>{
        console.log('gat-all-stock-faild');
        console.log(err);
        
      }
    )
  }
  onSupplierChoose(supplier:Supplier){
    debugger
    this.displayStock=true
    this.stockService.getAll()
    .subscribe(
      succ=>{
        this.stockService.AllProd=succ;
        debugger
        this.stockService.AllProd=this.stockService.AllProd.filter(
          stock=>stock.supplierId==supplier.supplierId
        )
      },
      err=>{
        console.log('get-all-stock-faild');
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
