import { Injectable } from '@angular/core';
import {  Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard {

  constructor(private router: Router) {}

  canActivate(): Observable<boolean> | Promise<boolean> | boolean {
    const token = localStorage.getItem('authToken');  // Verifique se o token está armazenado no localStorage
    if (token) {
      this.router.navigate(['/']); 
      return true;  // O usuário está autenticado
    } else {
      this.router.navigate(['/Account/login']);  // Redireciona para login se não autenticado
      return false;  // Não permite a navegação
    }
  }
}
