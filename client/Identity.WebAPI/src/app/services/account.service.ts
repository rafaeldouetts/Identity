import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegisterModel } from '../modules/account/model/RegisterModel';
import { LoginModel } from '../modules/account/model/LoginModel';
import { ChangePasswordModel } from '../modules/account/model/ChangePasswordModel';
import { ForgotPasswordModel } from '../modules/account/model/ForgotPasswordModel';
import { ResetPasswordModel } from '../modules/account/model/ResetPasswordModel';
import { UpdateProfileModel } from '../modules/account/model/UpdateProfileModel';
import { SendConfirmationTokenModel } from '../modules/account/model/SendConfirmationTokenModel';
import { ValidateTokenModel } from '../modules/account/model/ValidateTokenModel';
import { TwoFactorAuthModel } from '../modules/account/model/TwoFactorAuthModel';
import { ValidateTwoFactorAuthRequest } from '../modules/account/model/ValidateTwoFactorAuthRequest';
import { environment } from '../../environments/environment';
import { TokenModal } from '../modules/account/model/TokenModal';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private baseUrl = environment.identityUrl;
  private tokenKey = 'auth_token';

  constructor(private http: HttpClient) {}

  private getHeaders(): HttpHeaders {
    const token = this.getToken();
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', `Bearer ${token.value}`);
    }
    // Adicione outros cabeçalhos conforme necessário
    return headers;
  }

  register(data: RegisterModel): Observable<any> {
    return this.http.post(`${this.baseUrl}/register`, data, { headers: this.getHeaders() });
  }

  login(data: LoginModel): Observable<any> {
    return this.http.post(`${this.baseUrl}/login`, data);
  }

  logout(): Observable<any> {
    return this.http.post(`${this.baseUrl}/logout`, {}, { headers: this.getHeaders() });
  }

  changePassword(data: ChangePasswordModel): Observable<any> {
    return this.http.post(`${this.baseUrl}/change-password`, data, { headers: this.getHeaders() });
  }

  forgotPassword(data: ForgotPasswordModel): Observable<any> {
    return this.http.post(`${this.baseUrl}/forgot-password`, data, { headers: this.getHeaders() });
  }

  resetPassword(data: ResetPasswordModel): Observable<any> {
    return this.http.post(`${this.baseUrl}/reset-password`, data, { headers: this.getHeaders() });
  }

  updateProfile(data: UpdateProfileModel): Observable<any> {
    return this.http.put(`${this.baseUrl}/update-profile`, data, { headers: this.getHeaders() });
  }

  uploadProfilePicture(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('formFile', file);
    return this.http.post(`${this.baseUrl}/upload-profile-picture`, formData, { headers: this.getHeaders() });
  }

  sendEmailConfirmation(data: SendConfirmationTokenModel): Observable<any> {
    return this.http.post(`${this.baseUrl}/send-email-confirmation`, data, { headers: this.getHeaders() });
  }

  sendPhoneConfirmation(data: SendConfirmationTokenModel): Observable<any> {
    return this.http.post(`${this.baseUrl}/send-phone-confirmation`, data, { headers: this.getHeaders() });
  }

  validateEmailToken(data: ValidateTokenModel): Observable<any> {
    return this.http.post(`${this.baseUrl}/validate-email-token`, data, { headers: this.getHeaders() });
  }

  validatePhoneToken(data: ValidateTokenModel): Observable<any> {
    return this.http.post(`${this.baseUrl}/validate-phone-token`, data, { headers: this.getHeaders() });
  }

  sendTwoFactorCode(data: TwoFactorAuthModel): Observable<any> {
    return this.http.post(`${this.baseUrl}/send-2fa-code`, data, { headers: this.getHeaders() });
  }

  validateTwoFactorCode(data: ValidateTwoFactorAuthRequest): Observable<any> {
    return this.http.post(`${this.baseUrl}/validate-fa-code`, data, { headers: this.getHeaders() });
  }

  // Função para verificar se o usuário está logado
  isLoggedIn(): boolean {
    return localStorage.getItem('authToken') !== null;
  }

  setToken(token: TokenModal): void {
    localStorage.setItem(this.tokenKey, JSON.stringify(token));
  }

  getToken(): TokenModal | null {
    const token = localStorage.getItem(this.tokenKey);
    return token ? JSON.parse(token) : null;
  }
}
