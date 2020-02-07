import { Component, OnInit } from '@angular/core';
import { ProductService } from '../..';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {

  constructor(private test: ProductService) { }

  ngOnInit() {
    this.test.getAll().subscribe(s => console.log(s));

  }

}
