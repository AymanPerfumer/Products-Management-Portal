import { Component, OnInit } from '@angular/core';
import { Productervice } from '../../shared/services/product.service';

@Component({
  selector: 'app-show-product',
  templateUrl: './show-product.component.html',
  styleUrls: ['./show-product.component.css']
})
export class ShowProductComponent implements OnInit {

  constructor(private service: Productervice) { }

  TitleFilter: string = "";
  DescriptionFilter: string = "";
  ProductListWithoutFilter: any = [];
  ProductList: any = [];

  ModalTitle: string = "";
  ActivateAddEditEmpComp: boolean = false;
  ActivateDetails: boolean = false;
  product: any;

  ngOnInit(): void {
    this.refreshProductList();
  }

  addClick() {
    this.product = {
      ProductId: "",
      EmployeeName: "",
      Department: "",
      DateOfJoining: "",
      PhotoFileName: "anonymous.png"
    }
    this.ModalTitle = "Add Product";
    this.ActivateAddEditEmpComp = true;

  }

  detailClick(item: any) {
    console.log(item);
    this.product = item;
    this.ActivateDetails = true;
  }

  editClick(item: any) {
    console.log(item);
    this.product = item;
    this.ModalTitle = "Edit Product";
    this.ActivateAddEditEmpComp = true;
  }

  deleteClick(item: any) {
    if (confirm('Are you sure??')) {
      this.service.deleteProduct(item.id).subscribe(data => {
        alert(data.toString());
        this.refreshProductList();
      })
    }
  }

  closeClick() {
    this.ActivateAddEditEmpComp = false;
    this.refreshProductList();
  }

  closeDetailsClick() {
    this.ActivateDetails = false;
  }


  refreshProductList() {
    this.service.getProducts().subscribe(data => {
      this.ProductList = data;
      this.ProductListWithoutFilter = data;
    });
  }

  DescriptionFilterFn() {
    var DescriptionFilter = this.DescriptionFilter;

    this.ProductList = this.ProductListWithoutFilter.filter(function (el: any) {
      return el.description.toString().toLowerCase().includes(
        DescriptionFilter.toString().trim().toLowerCase()
      )
    });
  }

  TitleFilterFn() {
    var TitleFilter = this.TitleFilter;

    this.ProductList = this.ProductListWithoutFilter.filter(function (el: any) {
      return el.title.toString().toLowerCase().includes(
        TitleFilter.toString().trim().toLowerCase()
      )
    });
  }

}
