import {Component} from '@angular/core';
import {SomethingService} from '../something-service/something.service';
import {FormsModule} from '@angular/forms';
import {NgForOf, NgIf} from '@angular/common';

@Component({
  selector: 'app-something-rewrite',
  standalone: true,
  imports: [FormsModule, NgForOf, NgIf],
  templateUrl: './something-rewrite.component.html',
  styleUrl: './something-rewrite.component.css',
})
export class SomethingRewriteComponent {
  somethings: { code: number; value: string }[] = [{code: 0, value: ''}];
  errorMessage: string = '';
  successMessage: string = '';

  constructor(private readonly somethingService: SomethingService) {
  }

  addSomething(): void {
    this.somethings.push({code: 0, value: ''});
  }

  removeSomething(index: number): void {
    if (this.somethings.length > 1) {
      this.somethings.splice(index, 1);
    }
  }

  onSubmit(): void {
    this.errorMessage = '';
    this.successMessage = '';

    const isValid = this
      .somethings
      .every((item) => item.code !== 0 && item.value.trim() !== '');

    if (!isValid) {
      this.errorMessage = 'Не все поля заполнены!';
      return;
    }

    this.somethingService.rewriteSomethings(this.somethings).subscribe({
      next: () => {
        this.successMessage = 'Значения успешно добавлены!';
        this.somethings = [{code: 0, value: ''}];
      },
      error: (error) => {
        this.errorMessage = 'Ошибка при добавлении значений: ' + error.message;
        console.error('Ошибка перезаписи значений:', error);
      },
    });
  }
}
