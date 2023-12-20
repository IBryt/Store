import { Component, OnInit, OnDestroy } from '@angular/core';
import { ProductService } from 'src/app/services/products.service';
import { Subscription } from 'rxjs';
import { Product } from 'src/app/models/product/product';

@Component({
  selector: 'app-product-catalog-page',
  templateUrl: './product-catalog-page.component.html',
  styleUrls: ['./product-catalog-page.component.css']
})
export class ProductCatalogPageComponent implements OnInit, OnDestroy {
  products: Product[] = [];
  private subscription: Subscription = new Subscription();;

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    var r = this.productService.getProducts(1).subscribe((p) => {
      this.products = p;
    });
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
