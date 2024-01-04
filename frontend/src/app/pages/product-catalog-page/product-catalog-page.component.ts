import { Component, OnInit, OnDestroy } from '@angular/core';
import { ProductService } from 'src/app/services/products.service';
import { Subscription } from 'rxjs';
import { Product } from 'src/app/models/product/product';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-product-catalog-page',
  templateUrl: './product-catalog-page.component.html',
  styleUrls: ['./product-catalog-page.component.css']
})
export class ProductCatalogPageComponent implements OnInit, OnDestroy {
  products: Product[] = [];
  private subscription: Subscription = new Subscription();

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.subscription.add(
      this.route.queryParams.subscribe((params: Params) => {
        this.loadData(params);
      })
    );
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  private loadData(params: Params): void {
    this.subscription.add(
      this.productService.getProducts(params).subscribe((p) => {
        this.products = p;
      })
    );
  }
}