import { Component, OnInit } from '@angular/core';
import { SomethingService } from '../something-service/something.service';
import { SomethingPaged } from '../something-service/dto/SomethingPaged';
import { NgIf } from '@angular/common';
import { PaginatorModule } from 'primeng/paginator';
import { TableModule } from 'primeng/table';

@Component({
  selector: 'app-something-paged',
  standalone: true,
  imports: [
    PaginatorModule,
    NgIf,
    TableModule,
  ],
  templateUrl: './something-paged.component.html',
  styleUrl: './something-paged.component.css',
})
export class SomethingPagedComponent implements OnInit {
  somethingPaged!: SomethingPaged;
  first: number = 0;
  rows: number = 5;
  totalCount: number = 0;

  constructor(private readonly somethingService: SomethingService) {}

  ngOnInit(): void {
    this.loadSomethings(1, this.rows);
  }

  onPageChange(event: any): void {
    this.first = event.first;
    this.rows = event.rows;

    const pageNumber = (event.first / event.rows) + 1;
    this.loadSomethings(pageNumber, this.rows);
  }

  private loadSomethings(pageNumber: number, pageSize: number): void {
    this.somethingService.getSomethings(pageNumber, pageSize).subscribe({
      next: (response: SomethingPaged) => {
        this.somethingPaged = response;
        this.totalCount = response.totalCount;
        console.log('Записей на странице:', response.somethings.length, 'Всего:', response.totalCount);
      },
      error: (error) => {
        console.error('Ошибка загрузки значений:', error);
      },
    });
  }
}
