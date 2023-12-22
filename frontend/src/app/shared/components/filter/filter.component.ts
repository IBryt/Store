import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, NavigationExtras, Router } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';
import { Category } from 'src/app/models/category/category';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.css']
})
export class FilterComponent implements OnInit, OnDestroy {
  categories: Category[] = [];
  selectedValue: Category | undefined;

  private subscription: Subscription = new Subscription();

  constructor(
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.subscription.add(
      this.categoryService.getCategories()
        .subscribe(c => {
          this.categories = c
          this.categories.unshift({ categoryName: "All" } as Category);
        })
    );
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
  selectCategory(category?: Category): void {
    if (this.selectedValue != category) {
      this.selectedValue = category;

      this.router.navigate([], {
        relativeTo: this.route,
        queryParams: {
          CategoryId: category?.id,
          Page: 1
        },
        queryParamsHandling: 'merge',
      });
    }
  }
}
