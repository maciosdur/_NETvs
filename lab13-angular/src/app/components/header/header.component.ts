import { Component } from '@angular/core';
import { RouterModule } from '@angular/router'; 

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterModule], 
  template: `
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark mb-3">
      <div class="container-fluid">
        <a class="navbar-brand" routerLink="/">Sklep Lab14</a>
        
        <div class="collapse navbar-collapse">
          <ul class="navbar-nav me-auto mb-2 mb-lg-0">
            
            <li class="nav-item">
              <a class="nav-link" routerLink="/articles" routerLinkActive="active">Artykuły</a>
            </li>

            <li class="nav-item">
              <a class="nav-link" routerLink="/categories" routerLinkActive="active">Kategorie</a>
            </li>

          </ul>
        </div>
      </div>
    </nav>
  `
})
export class HeaderComponent {}
