import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokenModal } from '../modules/account/model/TokenModal';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Recupera o token do localStorage
    const token = this.getToken();

    debugger
    // Clona a requisição e adiciona cabeçalhos necessários
    const authReq = req.clone({
      // setHeaders: {
      //   'Content-Type': 'application/json', // Cabeçalho para aceitar resposta em JSON
      //   ...(token ? { 'Authorization': `Bearer ${token.value}` } : {}) // Condicionalmente adiciona o token
      // }
    });

    // Passa a requisição modificada adiante
    return next.handle(authReq);
  }

  getToken(): TokenModal | null {
    const token = localStorage.getItem(`auth_token`);
    return token ? JSON.parse(token) : null;
  }
}
