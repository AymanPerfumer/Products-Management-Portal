import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './auth.guard';
import { CategoryComponent } from './category/category.component';
import { NotFoundComponent } from './error-pages/not-found/not-found.component';
import { HomeComponent } from './home/home.component';
import { ProductComponent } from './product/product.component';


const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'category', component: CategoryComponent, canActivate: [AuthGuard] },
  { path: 'product', component: ProductComponent, canActivate: [AuthGuard] },
  { path: 'authentication', loadChildren: () => import('./authentication/authentication.module').then(m => m.AuthenticationModule) },
  { path: '404', component: NotFoundComponent },
  { path: '**', redirectTo: '/404', pathMatch: 'full' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
