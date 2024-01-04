import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { CartItem } from 'src/app/models/cart/cart-item';
import { Product } from 'src/app/models/product/product';
import { CartService } from 'src/app/services/cart.service';
import { ProductService } from 'src/app/services/products.service';

@Component({
  selector: 'app-cart-page',
  templateUrl: './cart-page.component.html',
  styleUrls: ['./cart-page.component.css']
})
export class CartPageComponent implements OnInit {

  items: CartItem[] = [];

  constructor(
    private cartService: CartService,
    private productService: ProductService,
  ) { }

  ngOnInit(): void {
    this.GetCartItems();
  }

  GetCartItems(): void {
    const cart = this.cartService.getCartItems();
    //const ids = Array.from(cart.keys());
    const cartItemsArray: CartItem[] = [];

    cart.forEach((qty, productId) => {
      const product$: Observable<Product> = this.productService.getProductById(productId);

      product$.subscribe(
        (product: Product) => {
          cartItemsArray.push({ product, qty });
        },
        (error) => {
          console.error(`Error fetching product with ID ${productId}: ${error}`);
        }
      );
    });
    this.items = cartItemsArray;
  }

  decreaseQuantity(item: CartItem) {
    if (item.qty > 0) {
      item.qty--;
      this.updateCart();
    }
  }

  increaseQuantity(item: CartItem) {
    item.qty++;
    this.updateCart();
  }

  removeItem(index: number) {
    this.items.splice(index, 1);
    this.updateCart();
  }

  getTotalQuantity(): number{
    return this.items.reduce((total, item) => total + item.qty * item.product.price, 0);
  }

  confirmOrder() {

  }

  private updateCart() {
    this.cartService.update(this.items);
  }

}
