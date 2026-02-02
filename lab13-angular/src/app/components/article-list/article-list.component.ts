import { Component, inject } from '@angular/core';
import { ArticleService } from '../../services/article.service';
import { ArticleItemComponent } from '../article-item/article-item';

@Component({
  selector: 'app-article-list',
  standalone: true,
  imports: [ArticleItemComponent],
  template: `
    <div class="mt-4">
      <h3>Lista Artykułów</h3>
      <ul class="list-group">
        @for (item of articleService.articles(); track item.id) {
          <app-article-item 
            [data]="item" 
            (remove)="delete($event)">
          </app-article-item>
        } @empty {
          <p>Brak artykułów w sklepie.</p>
        }
      </ul>
    </div>
  `
})
export class ArticleListComponent {
  articleService = inject(ArticleService); 
  articles = this.articleService.articles;
  delete(id: number) {
  this.articleService.deleteArticle(id).subscribe({
    next: () => {
      console.log('Usunięto artykuł z bazy');
    },
    error: (err) => alert('Błąd usuwania: ' + err.message)
  });
}
}