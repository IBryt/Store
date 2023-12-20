import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent {
  private currentPage: number = 1;
  private totalPages: number = 10;

  constructor(private router: Router) { }

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
    this.router.navigate(['/products'], { queryParams: { Page: page } });
  }
}
