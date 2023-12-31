import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';
import { environment } from 'src/app/environments/environment';
import { Product } from 'src/app/models/product/product';
import { CartService } from 'src/app/services/cart.service';
import { ProductService } from 'src/app/services/products.service';

@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrls: ['./product-page.component.css']
})
export class ProductPageComponent implements OnInit, OnDestroy {

  product: Product = {} as Product;
  private subProductService: Subscription = new Subscription();

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private cartService: CartService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      const productId = params['id'];
      this.subProductService.add(
        this.productService.getProductById(productId).subscribe((p) => {
          this.product = p;
        })
      );
    });
  }

  ngOnDestroy(): void {
    if (this.subProductService) {
      this.subProductService.unsubscribe();
    }
  }

  getImageUrl() {
    if (!this.product.imageUrl)
      return 'https://via.placeholder.com/300'
    return environment.baseUrl + '/productImages/' + this.product.imageUrl
  }

  add() {
    this.cartService.addToCart(this.product)
  }
}
