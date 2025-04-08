import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { SupplierService } from '../../services/supplier.service';
import { Supplier } from '../../classes/Supplier';
import { HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Stock } from '../../classes/Stock';
import { StockService } from '../../services/stock.service';
import { forkJoin, tap } from 'rxjs';
@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule,MatFormFieldModule, MatInputModule, MatButtonModule,ReactiveFormsModule,HttpClientModule],
  providers: [SupplierService,StockService],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{
  form: FormGroup | any;
  newSupplier:Supplier = {
    supplierId: 0,
    supllierName: '',
    companyName: '',
    phoneNumber: '',
    representative: '',
    stocks: []
  };
  constructor(public router:Router,private fb: FormBuilder,public supplierService: SupplierService,public stockService:StockService) {}
  get prods(): FormArray {
    return this.form.get('prods') as FormArray;
  }
  ngOnInit(): void {
    this.form = this.fb.group({
      company: ['', Validators.required],
      phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      representative: ['', Validators.required],
      numOfProds: [0, Validators.required],
      prods: this.fb.array([])
    });
    this.form.get('numOfProds')?.valueChanges.subscribe((value:number|any)=> {
      this.onNumberChange(value);
    });
  }

    send(): void {
      if (this.form!.valid) {
        debugger  
        this.newSupplier!.companyName=this.form.get("company").value;
        this.newSupplier!.phoneNumber=this.form.get("phoneNumber").value;
        this.newSupplier!.representative=this.form.get("representative").value;
        const stocks: Stock[] = this.form.get('prods').value.map((prod: any) => ({
            prodId: 0,
            prodName: prod.name,
            price: prod.price,
            minAmount:prod.minAmount
          }));
          const stockObservables = stocks.map(stock =>
            this.stockService.addStock(stock).pipe(
              tap(succ => {
                debugger
                this.newSupplier.stocks.push(succ);
              })
            )
          );
          
          forkJoin(stockObservables).subscribe({
            next: () => {
              console.log('All stocks added successfully');
            },
            error: err => {
              debugger
              console.log('Error adding stocks:', err);
            }
          });
        this.supplierService.addSupplier(this.newSupplier!)
        .subscribe(
          succ=>{         
            this.router.navigate(['']);
          },
          err=>{
            console.log('add-supplier-failed');
          }
        )
      } else {
        console.log('Form is invalid');
      }
    }
    onNumberChange(numOfProds:number){
      const prodsArray = this.form.get('prods') as FormArray;
      prodsArray.clear();     
      for (let i = 0; i < numOfProds; i++) {
        prodsArray.push(
          this.fb.group({
            name: ['', Validators.required],
            price: [1, [Validators.required, Validators.min(1)]],
            minAmount: [1, [Validators.required]]
          })
        );
      }
    }

}
