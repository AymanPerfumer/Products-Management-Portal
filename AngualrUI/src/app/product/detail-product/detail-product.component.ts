import { Component, Input, OnInit } from '@angular/core';
import { Productervice } from '../../shared/services/product.service';

@Component({
  selector: 'app-detail-product',
  templateUrl: './detail-product.component.html',
  styleUrls: ['./detail-product.component.css']
})
export class DetailProductComponent implements OnInit {

  constructor(private productService: Productervice) { }

  @Input() product: any;
  PhotoFilePath: string = "";

  ngOnInit(): void {
    this.PhotoFilePath = this.productService.PhotoUrl + this.product.image;
  }

}
