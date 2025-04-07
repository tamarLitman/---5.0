import { Routes } from '@angular/router';
import { RegisterComponent } from '../comps/register/register.component';
import { NavComponent } from '../comps/nav/nav.component';
import { AllOrdersComponent } from '../comps/all-orders/all-orders.component';
import { AddOrderComponent } from '../comps/add-order/add-order.component';

export const routes: Routes = [
    { path: '', component: NavComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'allOrders', component: AllOrdersComponent },
    { path: 'addOrder', component: AddOrderComponent },
];
