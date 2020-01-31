import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerListComponent } from './components/customer-list/customer-list.component';
import { CustomerDetailComponent } from './components/customer-detail/customer-detail.component';



@NgModule({
  declarations: [CustomerListComponent, CustomerDetailComponent],
  imports: [
    CommonModule
  ]
})
export class CustomerModule { }
