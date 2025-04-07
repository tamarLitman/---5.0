import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { SupplierService } from '../../services/supplier.service';
import { Supplier } from '../../classes/Supplier';
import { HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';
@Component({
  selector: 'app-register',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, MatButtonModule,ReactiveFormsModule,HttpClientModule],
  providers: [SupplierService],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{
  form: FormGroup | any;
  newSupplier:Supplier = {
    SupplierId: 0,
    SupllierName: '',
    CompanyName: '',
    PhoneNumber: '',
    Representative: '',
    Stocks: []
  };
  constructor(public router:Router,private fb: FormBuilder,public supplierService: SupplierService) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      company: ['', Validators.required],
      phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      representative: ['', Validators.required]
    });
  }

    send(): void {
      if (this.form!.valid) {
        debugger
        console.log(this.form);
        console.log(this.form.get("company").value);  
        this.newSupplier!.CompanyName=this.form.get("company").value;
        this.newSupplier!.PhoneNumber=this.form.get("phoneNumber").value;
        this.newSupplier!.Representative=this.form.get("representative").value;
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
  
}
