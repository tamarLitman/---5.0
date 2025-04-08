import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';
import { SupplierService } from '../../services/supplier.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { HttpClientModule } from '@angular/common/http';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-nav',
  standalone: true,
  providers: [SupplierService], 
  imports: [RouterModule,MatFormFieldModule, MatInputModule, MatButtonModule,ReactiveFormsModule,HttpClientModule,MatCardModule, MatButtonModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent implements OnInit {

  constructor(public router:Router,private fb: FormBuilder,public supplierService:SupplierService) {
 
  }
 
   ngOnInit(): void {
     this.form = this.fb.group({
      supplierId: ['', Validators.required],

     });
   }
  form: FormGroup | any;
  supplierId:number =0;

  send(): void {
    debugger
    if (this.form!.valid) {
      console.log(this.form);
      console.log(this.form.get("supplierId").value);  
      this.supplierId=Number(this.form.get("supplierId").value);
      this.supplierService.getByCode(this.supplierId)
      .subscribe(
        succ=>{
          if(succ==null){
            console.log("supplier not exists");
            this.router.navigate(['/register']);
          }
          else{
            debugger
            sessionStorage.setItem("supplierId",this.form.get("supplierId").value);
            sessionStorage.setItem("isGrocer","false");
            this.router.navigate(['/allOrders']);
          }
        },
        err=>{
          console.log('add-supplier-failed');
        }
      )
    } else {
      console.log('Form is invalid');
    }
  }
  grocerLogin(){
    sessionStorage.setItem("isGrocer","true")
    this.router.navigate(['/allOrders'])
  }

}
