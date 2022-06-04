import { Component, Input, OnInit } from '@angular/core';
import { CategoryService } from '../../shared/services/category.service';
import { Productervice } from '../../shared/services/product.service';

@Component({
  selector: 'app-add-edit-product',
  templateUrl: './add-edit-product.component.html',
  styleUrls: ['./add-edit-product.component.css']
})
export class AddEditProductComponent implements OnInit {

  constructor(
    private productService: Productervice,
    private categoryService: CategoryService)
  { }

  @Input() product: any;
  ProductId: string = "";
  Title: string = "";
  Description: string = "";
  Category: string = "";
  Price: number = 0;
  DateOfJoining: string = "";
  PhotoFileName: string = "";
  PhotoFilePath: string = "";

  CategoriesList: any = [];

  ngOnInit(): void {
    this.loadCategoriesList();
  }

  loadCategoriesList() {
    this.categoryService.getCategories().subscribe((data: any) => {
      this.CategoriesList = data;
    });

      this.ProductId = this.product.id;
      this.Title = this.product.title;
      this.Description = this.product.description;
      this.Category = this.product.category;
      this.Price = this.product.price;
      this.PhotoFileName = this.product.image == undefined ? "anonymous.png" : this.product.image;
      this.PhotoFilePath = this.productService.PhotoUrl + this.PhotoFileName;
    
  }

  addProduct() {
    var val = {
      Title: this.Title,
      Description: this.Description,
      Category: this.Category,
      Price: this.Price,
      Image: this.PhotoFileName
    };

    this.productService.addProduct(val).subscribe(res => {
      alert("Product added successfully!");
    });
  }

  updateProduct() {
    var val = {
      Id: this.ProductId,
      Title: this.Title,
      Description: this.Description,
      Category: this.Category,
      Price: this.Price,
      Image: this.PhotoFileName
    };

    this.productService.updateProduct(val).subscribe(res => {
      alert("Product updated successfully!");
    });
  }


  uploadPhoto(event: any) {
    var file = event.target.files[0];
    const formData: FormData = new FormData();
    formData.append('uploadedFile', file, file.name);

    this.productService.UploadPhoto(formData).subscribe((data: any) => {
      this.PhotoFileName = data.filename;
      this.PhotoFilePath = this.productService.PhotoUrl + this.PhotoFileName;
    })
  }

}
