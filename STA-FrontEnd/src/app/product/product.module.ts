import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductListComponent } from './components/product-list/product-list.component';
import { ProductDetailComponent } from './components/product-detail/product-detail.component';



@NgModule({
  declarations: [ProductListComponent, ProductDetailComponent],
  imports: [
    CommonModule
  ]
})
export class ProductModule { }
