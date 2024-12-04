import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './modules/core/home/home.component';  // Ajuste o caminho se necessário
import { AuthGuard } from './auth/auth.guard';

const routes: Routes = [
  // Rota preguiçosa para carregar o módulo de conta
  { 
    path: 'account', 
    loadChildren: () => import('./modules/account/account.module').then(m => m.AccountModule) 
  },
  
  // Página principal, protegida pelo AuthGuard
  { 
    path: '', 
    component: HomeComponent, 
    canActivate: [AuthGuard] 
  },
  
  // Caso a rota não exista, redireciona para o login
  { 
    path: '**', 
    redirectTo: '/account/login' 
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
