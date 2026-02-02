import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ArticleService } from '../../services/article.service';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [FormsModule],
  template: `
    <div class="container mt-4">
      <h2>Zarządzanie Kategoriami</h2>

      <div class="card p-3 mb-4 bg-light">
        <div class="input-group">
          <input 
            type="text" 
            class="form-control" 
            placeholder="Nazwa nowej kategorii..." 
            [(ngModel)]="newCategoryName">
          <button class="btn btn-success" (click)="add()">+ Dodaj</button>
        </div>
      </div>

      <table class="table table-striped table-hover shadow-sm">
        <thead class="table-dark">
          <tr>
            <th>ID</th>
            <th>Nazwa</th>
            <th style="width: 100px;">Akcje</th>
          </tr>
        </thead>
        <tbody>
          @for (cat of articleService.availableCategories(); track cat.id) {
            <tr>
              <td>{{ cat.id }}</td>
              <td><strong>{{ cat.name }}</strong></td>
              <td>
                <button class="btn btn-danger btn-sm" (click)="delete(cat.id)">Usuń</button>
              </td>
            </tr>
          } @empty {
            <tr>
              <td colspan="3" class="text-center">Brak kategorii w bazie.</td>
            </tr>
          }
        </tbody>
      </table>
    </div>
  `
})
export class CategoryListComponent {
  articleService = inject(ArticleService);
  newCategoryName = '';

  add() {
    if (!this.newCategoryName.trim()) return;

    this.articleService.addCategory(this.newCategoryName).subscribe({
      next: () => {
        this.newCategoryName = ''; // Wyczyść pole po sukcesie
        console.log('Kategoria dodana');
      },
      error: (err) => alert('Błąd dodawania kategorii: ' + err.message)
    });
  }

  delete(id: number) {
    if(confirm('Czy na pewno chcesz usunąć tę kategorię?')) {
      this.articleService.deleteCategory(id).subscribe({
        next: () => console.log('Kategoria usunięta'),
        error: (err) => alert('Nie można usunąć kategorii (być może są do niej przypisane artykuły!).')
      });
    }
  }
}