import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/internal/Subscription';
import { Category } from 'src/app/models/category/category';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.css']
})
export class FilterComponent implements OnInit, OnDestroy{
  categories : Category[] = [];
  selectedValue: Category | undefined;

  private subscription: Subscription = new Subscription();
  constructor(
    private categoryService: CategoryService
    ) { }

  ngOnInit(): void {
    this.subscription.add(
      this.categoryService.getCategories()
      .subscribe(c => this.categories = c) 
    );
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
  selectCategory(category: Category): void {
    this.selectedValue = category;
    
  }
}
