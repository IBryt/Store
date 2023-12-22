import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';
import { ProductService } from 'src/app/services/products.service';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnInit, OnDestroy {
  private currentPage: number = 1;
  private totalPages: number = 1;
  private subscription: Subscription = new Subscription();

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private productService: ProductService,
  ) { }

  ngOnInit(): void {
    this.subscription.add(
      this.route.queryParams.subscribe((params: Params) => {
        this.productService.getPagesCount(params)
          .subscribe(c => { this.totalPages = c })
      })

    );
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  get pages(): number[] {
    const totalButtonsToShow = 5;
    const pages = [];

    if (this.totalPages <= totalButtonsToShow) {
      for (let i = 1; i <= this.totalPages; i++) {
        pages.push(i);
      }
    } else {
      for (let i = 1; i <= totalButtonsToShow; i++) {
        let page = this.currentPage - Math.floor(totalButtonsToShow / 2) + i;
        if (this.currentPage <= Math.ceil(totalButtonsToShow / 2)) {
          page = i;
        } else if (this.currentPage > this.totalPages - Math.floor(totalButtonsToShow / 2)) {
          page = this.totalPages - totalButtonsToShow + i;
        }
        if (page >= 1 && page <= this.totalPages) {
          pages.push(page);
        }
      }
    }

    return pages;
  }

  getCurrentPage() {
    return this.currentPage;
  }

  getTotalPages() {
    return this.totalPages;
  }

  prevPage() {
    if (this.currentPage > 1) {
      this.navigateToPage(this.currentPage - 1);
    }
  }

  nextPage() {
    if (this.currentPage < this.totalPages) {
      this.navigateToPage(this.currentPage + 1);
    }
  }

  goToPage(page: number) {
    if (this.currentPage !== page) {
      this.navigateToPage(page);
    }
  }

  isCurrentPage(page: number): boolean {
    return this.currentPage === page;
  }

  private navigateToPage(page: number): void {
    this.currentPage = page;
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {
        Page: page
      },
      queryParamsHandling: 'merge',
    });
  }
}
