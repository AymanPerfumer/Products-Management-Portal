import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../shared/services/category.service';

@Component({
  selector: 'app-show-category',
  templateUrl: './show-category.component.html',
  styleUrls: ['./show-category.component.css']
})
export class ShowCategoryComponent implements OnInit {

  constructor(private service: CategoryService) { }

  CategoriesList: any = [];

  ModalTitle: string = "";
  ActivateAddEditDepComp: boolean = false;
  category: any;

  TitleFilter: string = "";
  CategoriesListWithoutFilter: any = [];

  ngOnInit(): void {
    this.refreshDepList();
  }

  addClick() {
    this.category = {
      Id: 0,
      Title: ""
    }
    this.ModalTitle = "Add Category";
    this.ActivateAddEditDepComp = true;

  }

  editClick(item: any) {
    this.category = item;
    this.ModalTitle = "Edit Category";
    this.ActivateAddEditDepComp = true;
  }

  deleteClick(item: any) {
    if (confirm('Are you sure??')) {
      this.service.deleteCategory(item.id).subscribe(data => {
        alert(data.toString());
        this.refreshDepList();
      })
    }
  }

  closeClick() {
    this.ActivateAddEditDepComp = false;
    this.refreshDepList();
  }


  refreshDepList() {
    this.service.getCategories().subscribe(data => {
      this.CategoriesList = data;
      this.CategoriesListWithoutFilter = data;
    });
  }

  FilterFn() {
    var TitleFilter = this.TitleFilter;

    this.CategoriesList = this.CategoriesListWithoutFilter.filter(function (el: any) {
      return el.name.toString().toLowerCase().includes(
          TitleFilter.toString().trim().toLowerCase()
        )
    });
  }

}
