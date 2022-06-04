import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { CategoryComponent } from './category/category.component';
import { ShowCategoryComponent } from './category/show-category/show-category.component';
import { AddEditCategoryComponent } from './category/add-edit-category/add-edit-category.component';
import { ProductComponent } from './product/product.component';
import { ShowProductComponent } from './product/show-product/show-product.component';
import { AddEditProductComponent } from './product/add-edit-product/add-edit-product.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { MenuComponent } from './menu/menu.component';
import { NotFoundComponent } from './error-pages/not-found/not-found.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JwtModule } from "@auth0/angular-jwt";
import { AuthGuard } from './auth.guard';
import { HomeComponent } from './home/home.component';
import { CategoryService } from './shared/services/category.service';
import { Productervice } from './shared/services/product.service';
import { DetailCategoryComponent } from './category/detail-category/detail-category.component';
import { DetailProductComponent } from './product/detail-product/detail-product.component';

export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    NotFoundComponent,
    CategoryComponent,
    ShowCategoryComponent,
    AddEditCategoryComponent,
    ProductComponent,
    ShowProductComponent,
    AddEditProductComponent,
    HomeComponent,
    DetailCategoryComponent,
    DetailProductComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:4002"],
        disallowedRoutes: []
      }
    })
  ],
  providers: [CategoryService, Productervice, AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
