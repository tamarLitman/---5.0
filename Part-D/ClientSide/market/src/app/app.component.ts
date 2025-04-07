import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AllOrdersComponent } from '../comps/all-orders/all-orders.component';
import { RegisterComponent } from '../comps/register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NavComponent } from '../comps/nav/nav.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,AllOrdersComponent,RegisterComponent,NavComponent,FormsModule,ReactiveFormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'market';
}
