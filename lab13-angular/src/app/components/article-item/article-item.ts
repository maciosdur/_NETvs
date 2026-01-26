import { Component, input, output } from '@angular/core';
import { Article } from '../../models/article.model';

@Component({
  selector: 'app-article-item',
  standalone: true,
  template: `
    <li class="list-group-item d-flex justify-content-between align-items-center">
      <span><strong>{{ data().name }}</strong> ({{ data().category }})</span>
      <button class="btn btn-danger btn-sm" (click)="onDelete()">Usuń</button>
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
