import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './src/app/app.component';
import { AllOrdersComponent } from './src/comps/all-orders/all-orders.component';
import {  HttpClientModule } from '@angular/common/http';
import { RegisterComponent } from './src/comps/register/register.component';
import { NavComponent } from './src/comps/nav/nav.component';

@NgModule({
  declarations: [
    AppComponent,
    AllOrdersComponent,
    RegisterComponent,
    NavComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    HttpClientModule 
  ],

  
  bootstrap: [AppComponent]
})
export class AppModule { }
