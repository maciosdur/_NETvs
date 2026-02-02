import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Article, Category } from '../models/article.model';
import { tap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ArticleService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7207/api/ArticlesApi'; 
  private categoriesUrl = 'https://localhost:7207/api/CategoriesApi';

  private articlesSignal = signal<Article[]>([]);
  articles = this.articlesSignal.asReadonly();
  
  availableCategories = signal<Category[]>([]);

  constructor() {
    this.loadArticles(); 
    this.loadCategories(); 
  }

  loadArticles() {
    this.http.get<Article[]>(`${this.apiUrl}?skip=0&take=100`).subscribe({
      next: (data) => this.articlesSignal.set(data),
      error: (err) => console.error('Błąd artykułów:', err)
    });
  }

  getArticleById(id: string) {
    return this.http.get<Article>(`${this.apiUrl}/${id}`);
  }

  addArticle(article: any) {
    return this.http.post<Article>(this.apiUrl, article).pipe(
      tap(() => this.loadArticles()) 
    );
  }

  deleteArticle(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.loadArticles())
    );
  }


  loadCategories() {
    this.http.get<Category[]>(this.categoriesUrl).subscribe({
      next: (data) => this.availableCategories.set(data),
      error: (err) => console.error('Błąd kategorii:', err)
    });
  }


  addCategory(name: string) {
    const newCategory = { id: 0, name: name };
    return this.http.post<Category>(this.categoriesUrl, newCategory).pipe(
      tap(() => this.loadCategories()) 
    );
  }


  deleteCategory(id: number) {
    return this.http.delete(`${this.categoriesUrl}/${id}`).pipe(
      tap(() => this.loadCategories())
    );
  }

  updateArticle(id: number, article: any) {
    return this.http.put(`${this.apiUrl}/${id}`, article).pipe(
      tap(() => this.loadArticles())
    );
  }
}