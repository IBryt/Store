import { Component, Input } from '@angular/core';
import { environment } from 'src/app/environments/environment';
import { Product } from 'src/app/models/product/product';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent {
  @Input() product: Product = {} as Product;

  getImageUrl() {
    if(!this.product.imageUrl)
      return 'https://via.placeholder.com/300'
    return environment.baseUrl + '/productImages/' + this.product.imageUrl
  }

}
