import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainLayoutComponent } from './shared/components/main-layout/main-layout.component';
import { ProductPageComponent } from './pages/product-page/product-page.component';
import { ProductCatalogPageComponent } from './pages/product-catalog-page/product-catalog-page.component';

const routes: Routes = [
  {
    path: '', component: MainLayoutComponent, children: [
      { path: '', redirectTo: '/products/1', pathMatch: 'full' },
      { path: 'products/:page', component: ProductCatalogPageComponent },
      { path: 'product/:id', component: ProductPageComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
