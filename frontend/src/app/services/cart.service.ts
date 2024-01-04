import { Injectable } from '@angular/core';
import { Product } from '../models/product/product';
import { CartItem } from '../models/cart/cart-item';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private cartKey = 'cart_store';

  constructor() {}

  getCartItems(): Map<number, number> {
    const storedCart = localStorage.getItem(this.cartKey);
    return storedCart ? new Map(JSON.parse(storedCart)) : new Map<number, number>();
  }


  addToCart(item: Product): void {
    let cartItems = this.getCartItems()
    const qty = cartItems.get(item.id) ?? 0;
    cartItems.set(item.id, qty + 1);
    this.updateCart(cartItems);
  }

  clearCart(): void {
    localStorage.removeItem(this.cartKey);
  }

  update(items: CartItem[]) {
    const cartMap = items.reduce((map, cartItem) => {
        const productId = cartItem.product.id;
        const qty = cartItem.qty;
      
        map.set(productId, qty);
        return map;
      }, new Map<number, number>());
      
      this.updateCart(cartMap);
  }

  private updateCart(cartItems: Map<number, number>): void {
    localStorage.setItem(this.cartKey, JSON.stringify(Array.from(cartItems.entries())));
  }
}
