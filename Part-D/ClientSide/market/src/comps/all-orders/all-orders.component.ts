import { Component, OnInit } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import { OrderService } from '../../services/order.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Order } from '../../classes/Order';
import { Router } from '@angular/router';

@Component({
  selector: 'all-orders',
  standalone: true,
  imports: [MatCardModule, MatButtonModule,HttpClientModule,CommonModule],
  providers: [OrderService],
  templateUrl: './all-orders.component.html',
  styleUrl: './all-orders.component.css',
})
export class AllOrdersComponent implements OnInit {
  isGrocer:boolean=false;
  constructor(public orderService:OrderService,public router:Router) {
    
  }
  ngOnInit(): void {
    this.orderService.getAll()
    .subscribe(
      succ=>{
        this.orderService.AllOrders=succ;
        if(sessionStorage.getItem("isGrocer")=="false"){
          this.isGrocer=false
          this.orderService.AllOrders=this.orderService.AllOrders
          .filter(order=>order.orderStateId==1 || order.orderStateId==2)
        }
        else{
          this.isGrocer=true
        }
      },
      err=>{
        console.log('gat-all-orders-faild');
        console.log(err);
      }
    )
  }
  approveOrder(order:Order){
    debugger
    if(sessionStorage.getItem("isGrocer")=="false"){
      order.orderStateId=2
    }
    else if(sessionStorage.getItem("isGrocer")=="true"){
      order.orderStateId=3
    }
    this.orderService.update(order)
    .subscribe(
      succ=>{
        console.log('update-order-succeed');
      },
      err=>{
        console.log('update-order-faild');
      })
  }
  newOrder(){
    this.router.navigate(['/addOrder']);
  }
}
