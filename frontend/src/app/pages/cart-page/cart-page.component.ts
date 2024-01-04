import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs/internal/Subscription';
import { CartItem } from 'src/app/models/cart/cart-item';
import { CartService } from 'src/app/services/cart.service';
import { ProductService } from 'src/app/services/products.service';

@Component({
  selector: 'app-cart-page',
  templateUrl: './cart-page.component.html',
  styleUrls: ['./cart-page.component.css']
})
export class CartPageComponent implements OnInit, OnDestroy {
  private subProductService: Subscription = new Subscription();
  items: CartItem[] = [];

  constructor(
    private cartService: CartService,
    private productService: ProductService,
  ) { }


  ngOnInit(): void {
    this.GetCartItems();
  }

  ngOnDestroy(): void {
    this.subProductService.unsubscribe();
  }

  private GetCartItems(): void {
    const cart = this.cartService.getCartItems();
    const ids = Array.from(cart.keys());

    if (ids.length === 0) {
      this.items = [];
      return;
    }

    this.subProductService.add(
      this.productService.getProductByIds(ids)
        .subscribe((products) => {
          const cartItemsArray: CartItem[] = [];

          cart.forEach((qty, productId, index) => {
            const product = products.find(p => p.id === productId);
  
            if (product) {
              cartItemsArray.push({ product, qty });
            } else {
              console.error(`Product with ID ${productId} not found.`);
            }
          });
  
          this.items = cartItemsArray;
        })
    )
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

  getTotalQuantity(): number {
    return this.items.reduce((total, item) => total + item.qty * item.product.price, 0);
  }

  confirmOrder() {

  }

  private updateCart() {
    this.cartService.update(this.items);
  }

}


