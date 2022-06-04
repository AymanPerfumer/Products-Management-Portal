import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class CategoryService {
  readonly APIUrl = "/api";

  constructor(private http: HttpClient) { }

  getCategories(): Observable<any[]> {
    return this.http.get<any>(this.APIUrl + '/Category');
  }

  addCategory(val: any) {
    return this.http.post(this.APIUrl + '/Category', val);
  }

  updateCategory(category: any) {
    return this.http.put(this.APIUrl + '/Category/' + category.id, category);
  }

  deleteCategory(val: any) {
    return this.http.delete(this.APIUrl + '/Category/' + val);
  }
}
