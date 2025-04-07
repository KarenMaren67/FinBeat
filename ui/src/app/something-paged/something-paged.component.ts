import { AfterViewInit, Component, ViewChild, OnInit } from '@angular/core';
import { SomethingService } from '../something-service/something.service';
import { Something } from '../something-service/dto/Something';
import { SomethingPaged } from '../something-service/dto/SomethingPaged';
import { NgIf } from '@angular/common';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-something-paged',
  standalone: true,
  imports: [
    NgIf,
    MatTableModule,
    MatPaginatorModule
  ],
  templateUrl: './something-paged.component.html',
  styleUrl: './something-paged.component.css',
})
export class SomethingPagedComponent implements OnInit, AfterViewInit {
  somethingPaged: SomethingPaged = { somethings: [], totalCount: 0 }; // Initialize with default values
  displayedColumns: string[] = ['id', 'code', 'value'];
  dataSource = new MatTableDataSource<Something>(this.somethingPaged.somethings);

  constructor(private readonly somethingService: SomethingService) {}

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngOnInit(): void {
    this.loadSomethings(1, 5); // Load initial data with default page size
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.paginator.page.subscribe((event: PageEvent) => {
      this.loadSomethings(event.pageIndex + 1, event.pageSize);
    });
  }

  private loadSomethings(pageNumber: number, pageItemsCount: number): void {
    this.somethingService.getSomethings(pageNumber, pageItemsCount).subscribe({
      next: (response: SomethingPaged) => {
        this.somethingPaged = response;
        this.dataSource.data = response.somethings; // Update dataSource with new data
        console.log('Items на странице:', response.somethings.length, 'Total:', response.totalCount);
      },
      error: (error) => {
        console.error('Ошибка при загрузке значений кодов:', error);
      },
    });
  }
}