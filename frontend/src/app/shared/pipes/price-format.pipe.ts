import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'priceFormat'
})
export class PriceFormatPipe implements PipeTransform {
  transform(value: number): string {
    if (value === null || isNaN(value)) {
      return '';
    }

    const formattedPrice = new Intl.NumberFormat('uk-UA', {
      style: 'currency',
      currency: 'UAH',
      minimumFractionDigits: 2,
      maximumFractionDigits: 2
    }).format(value);

    return formattedPrice;
  }
}
