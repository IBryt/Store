import { Product } from "../product/product";

export interface CartItem {
    product: Product,
    qty: number;
}