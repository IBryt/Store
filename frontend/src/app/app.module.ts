import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainLayoutComponent } from './shared/components/main-layout/main-layout.component';
import { ProductPageComponent } from './pages/product-page/product-page.component';
import { ProductComponent } from './shared/components/product/product.component';
import { ProductCatalogPageComponent } from './pages/product-catalog-page/product-catalog-page.component';

@NgModule({
  declarations: [
    AppComponent,
    MainLayoutComponent,
    ProductPageComponent,
    ProductComponent,
    ProductCatalogPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
