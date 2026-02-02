import { Component, input, output } from '@angular/core';
import { Article } from '../../models/article.model';
import { RouterModule } from '@angular/router'; 

@Component({
  selector: 'app-article-item',
  standalone: true,
  imports: [RouterModule], 
  template: `
    <li class="list-group-item d-flex justify-content-between align-items-center">
      <span><strong>{{ data().name }}</strong> ({{ data().category.name }})</span>
      
      <div>
        <a [routerLink]="['/articles', data().id]" class="btn btn-info btn-sm me-2 text-white">
          Szczegóły
        </a>

        <a [routerLink]="['/articles', data().id, 'edit']" class="btn btn-warning btn-sm me-2">
          Edytuj
        </a>
        
        <button class="btn btn-danger btn-sm" (click)="onDelete()">Usuń</button>
      </div>
    </li>
  `
})
export class ArticleItemComponent {
  data = input.required<Article>(); 
  remove = output<number>();

  onDelete() {
    this.remove.emit(this.data().id);
  }
}