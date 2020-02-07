import { Component, OnInit } from '@angular/core';
import { ProductService } from './product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  constructor(private test: ProductService) {
    
  }

  ngOnInit() {
    this.test.getAll().subscribe(s => console.log(s));
  }
}
