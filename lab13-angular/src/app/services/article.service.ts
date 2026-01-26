import { Injectable, signal } from '@angular/core';
import { Article, Category } from '../models/article.model';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {
 
  readonly availableCategories: Category[] = ['Owoce', 'Warzywa', 'Nabiał', 'Inne'];

  private articlesSignal = signal<Article[]>([
    { id: 1, name: 'Jabłko', category: 'Owoce', price: 2.50 },
    { id: 2, name: 'Marchew', category: 'Warzywa', price: 1.80 }
  ]);

 
  articles = this.articlesSignal.asReadonly();

  addArticle(article: Omit<Article, 'id'>) {
    const newArticle = { ...article, id: Date.now() };
    this.articlesSignal.update(current => [...current, newArticle]);
  }

  deleteArticle(id: number) {
    this.articlesSignal.update(current => current.filter(a => a.id !== id));
  }
}