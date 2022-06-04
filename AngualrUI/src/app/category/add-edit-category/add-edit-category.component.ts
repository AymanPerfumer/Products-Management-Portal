import { Component, Input, OnInit } from '@angular/core';
import { CategoryService } from '../../shared/services/category.service';

@Component({
  selector: 'app-add-edit-category',
  templateUrl: './add-edit-category.component.html',
  styleUrls: ['./add-edit-category.component.css']
})
export class AddEditCategoryComponent implements OnInit {

  constructor(private service: CategoryService) { }

  @Input() category: any;
  Id: string = "";
  Title: string = "";

  ngOnInit(): void {
    this.Id = this.category.id;
    this.Title = this.category.title;
  }

  addCategory() {
    var val = {
      Title: this.Title
    };
    this.service.addCategory(val).subscribe(res => {
      alert("category added successfully!");
    });
  }

  updateCategory() {
    var val = {
      id: this.Id,
      title: this.Title
    };
    this.service.updateCategory(val).subscribe(res => {
      alert("category updated successfully!");
    });
  }

}
