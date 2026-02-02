import { Component, inject, input, OnInit, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ArticleService } from '../../services/article.service';
import { Router } from '@angular/router'; // Do przekierowania po edycji

@Component({
  selector: 'app-article-form',
  standalone: true,
  imports: [FormsModule], 
  template: `
    <div class="card p-3 mb-4 shadow-sm">
      <h4>{{ isEditMode ? 'Edytuj artykuł' : 'Dodaj nowy artykuł' }}</h4>
      
      <div class="mb-2">
        <input [(ngModel)]="name" placeholder="Nazwa" class="form-control">
      </div>
      
      <div class="mb-3">
        <label class="form-label">Kategoria</label>
        <select class="form-select" [(ngModel)]="categoryId" name="category" required>
          <option [ngValue]="0" disabled selected>Wybierz kategorię</option>
          
          @for (cat of articleService.availableCategories(); track cat.id) {
            <option [ngValue]="cat.id">{{ cat.name }}</option>
          }
        </select>
      </div>
      
      <div class="mb-2">
        <input [(ngModel)]="price" type="number" placeholder="Cena" class="form-control">
      </div>

      <button (click)="save()" class="btn" [class.btn-success]="!isEditMode" [class.btn-warning]="isEditMode">
        {{ isEditMode ? 'Zapisz zmiany' : 'Dodaj do listy' }}
      </button>
    </div>
  `
})
export class ArticleFormComponent implements OnInit {
  articleService = inject(ArticleService);
  router = inject(Router);

  // Pobieramy ID z URLa (jeśli wchodzimy przez /articles/5/edit)
  // Wymaga włączenia withComponentInputBinding w app.config.ts!
  id = input<string>(); 

  saveEvent = output(); // Zmieniłem nazwę z 'save', żeby nie myliła się z funkcją
  
  name = '';
  categoryId: number = 0; 
  price = 0;

  // Flaga pomocnicza
  isEditMode = false;

  ngOnInit() {
    // Sprawdzamy, czy w URLu jest ID. Jeśli tak -> tryb edycji
    const articleId = this.id();
    
    if (articleId) {
      this.isEditMode = true;
      this.loadArticleData(articleId);
    }
  }

  loadArticleData(id: string) {
    this.articleService.getArticleById(id).subscribe({
      next: (article) => {
        // Wypełniamy formularz danymi z backendu
        this.name = article.name;
        this.price = article.price;
        // Backend zwraca obiekt category, musimy wyciągnąć ID do selecta
        this.categoryId = article.category ? article.category.id : 0;
      },
      error: (err) => console.error('Błąd pobierania do edycji', err)
    });
  }

  save() {
    if (this.name && this.price > 0 && this.categoryId > 0) {
      
      // Obiekt zgodny z C# (PascalCase)
      const payload = {
        Id: this.isEditMode ? Number(this.id()) : 0, // Przy edycji ważne jest ID!
        Name: this.name,
        Price: Number(this.price),
        CategoryId: Number(this.categoryId)
      };

      console.log('Wysyłam do API:', payload); 

      if (this.isEditMode) {
        // --- TRYB EDYCJI (PUT) ---
        this.articleService.updateArticle(Number(this.id()), payload as any)
          .subscribe({
            next: () => {
              console.log('Zaktualizowano pomyślnie');
              this.router.navigate(['/articles']); // Wracamy do listy
            },
            error: (err) => this.handleError(err)
          });

      } else {
        // --- TRYB DODAWANIA (POST) ---
        this.articleService.addArticle(payload as any)
          .subscribe({
            next: () => {
              this.resetForm();
              this.saveEvent.emit(); // Zamyka modal (jeśli używane w modalu)
              console.log('Dodano pomyślnie');
            },
            error: (err) => this.handleError(err)
          });
      }

    } else {
        alert("Uzupełnij wszystkie pola! Cena musi być > 0.");
    }
  }

  private resetForm() {
    this.name = ''; 
    this.price = 0;
    this.categoryId = 0;
  }

  private handleError(err: any) {
    console.error('Błąd backendu:', err);
    if (err.error && err.error.errors) {
       alert('Błąd walidacji: ' + JSON.stringify(err.error.errors));
    } else {
       alert('Wystąpił błąd zapisu.');
    }
  }
}