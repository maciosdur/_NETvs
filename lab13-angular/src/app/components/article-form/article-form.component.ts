import { Component, inject, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ArticleService } from '../../services/article.service';
import { Category } from '../../models/article.model';

@Component({
  selector: 'app-article-form',
  standalone: true,
  imports: [FormsModule], 
  template: `
    <div class="card p-3 mb-4 shadow-sm">
      <h4>Dodaj nowy artykuł</h4>
      <div class="mb-2">
        <input [(ngModel)]="name" placeholder="Nazwa" class="form-control">
      </div>
      <div class="mb-2">
        <select [(ngModel)]="category" class="form-select">
          @for (cat of articleService.availableCategories; track cat) {
            <option [value]="cat">{{ cat }}</option>
          }
        </select>
      </div>
      <div class="mb-2">
        <input [(ngModel)]="price" type="number" placeholder="Cena" class="form-control">
      </div>
      <button (click)="add()" class="btn btn-success w-100">Dodaj do listy</button>
    </div>
  `
})
export class ArticleFormComponent {
  articleService = inject(ArticleService);

  save = output()
  name = '';
  category: Category = 'Inne';
  price = 0;

  add() {
    if (this.name && this.price > 0) {
      this.articleService.addArticle({
        name: this.name,
        category: this.category,
        price: this.price
      });
      this.name = ''; 
      this.price = 0;
      this.save.emit();
    }
    
  }
}