import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainLayoutComponent } from './shared/components/main-layout/main-layout.component';
import { ProductPageComponent } from './pages/product-page/product-page.component';
import { ProductComponent } from './shared/components/product/product.component';
import { ProductCatalogPageComponent } from './pages/product-catalog-page/product-catalog-page.component';
import { PriceFormatPipe } from './shared/pipes/price-format.pipe';
import { PaginationComponent } from './shared/components/pagination/pagination.component';
import { FooterComponent } from './shared/components/footer/footer.component';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { FilterComponent } from './shared/components/filter/filter.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    MainLayoutComponent,
    ProductPageComponent,
    ProductComponent,
    ProductCatalogPageComponent,
    PriceFormatPipe,
    PaginationComponent,
    FooterComponent,
    NavbarComponent,
    FilterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
