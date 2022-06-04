import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class SharedService {
  readonly APIUrl = "/api";
  readonly PhotoUrl = "https://localhost:7117/Photos/";

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


  getProducts(): Observable<any[]> {
    return this.http.get<any>(this.APIUrl + '/Product');
  }

  addProduct(val: any) {
    return this.http.post(this.APIUrl + '/Product', val);
  }

  updateProduct(val: any) {
    return this.http.put(this.APIUrl + '/Product/' + val.Id, val);
  }

  deleteProduct(val: any) {
    return this.http.delete(this.APIUrl + '/Product/' + val);
  }


  UploadPhoto(val: any) {
    return this.http.post(this.APIUrl + '/Product/SaveFile', val);
  }
}
