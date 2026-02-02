import { Routes } from '@angular/router';
import { ArticleListComponent } from './components/article-list/article-list.component';
import { ArticleDetails } from './components/article-details/article-details';
import { CategoryListComponent } from './components/category-list/category-list.component'; 
import { ArticleFormComponent } from './components/article-form/article-form.component';

export const routes: Routes = [
  { path: '', redirectTo: 'articles', pathMatch: 'full' },
  { path: 'articles', component: ArticleListComponent },
  { path: 'articles/:id', component: ArticleDetails },
  { path: 'articles/:id/edit', component: ArticleFormComponent },
  
  { path: 'categories', component: CategoryListComponent } 
];