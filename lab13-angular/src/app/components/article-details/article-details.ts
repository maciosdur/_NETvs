import { Component, inject, input, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ArticleService } from '../../services/article.service';
import { Article } from '../../models/article.model';

@Component({
  selector: 'app-article-details',
  standalone: true,
  // Dodajemy CommonModule (dla pipe'ów) i RouterModule (dla linków)
  imports: [CommonModule, RouterModule],
  templateUrl: './article-details.html',
  styleUrl: './article-details.css',
})
export class ArticleDetails implements OnInit {
  // Ten input automatycznie pobierze "id" z adresu URL
  id = input.required<string>(); 
  
  private articleService = inject(ArticleService);
  
  // Sygnał przechowujący dane artykułu
  article = signal<Article | null>(null);

  ngOnInit() {
    const articleId = this.id();
    this.articleService.getArticleById(articleId).subscribe({
      next: (data) => this.article.set(data),
      error: (err) => console.error('Błąd pobierania artykułu', err)
    });
  }
}