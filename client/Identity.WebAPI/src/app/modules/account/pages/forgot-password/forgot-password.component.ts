import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../../../../services/account.service';
import { ForgotPasswordModel } from '../../model/ForgotPasswordModel';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css'
})
export class ForgotPasswordComponent {
  email: string = '';
  successMessage: string = '';
  errorMessage: string = '';
  forgotform: FormGroup;

  constructor(private fb: FormBuilder, private accountService: AccountService, private router: Router) {
    this.forgotform = this.fb.group({
      email: ['', [Validators.required]],
    });
  }
  // Método para solicitar a recuperação da senha
  onSubmit() {
    if(this.forgotform.valid)
    {
      debugger

      var forgot = new ForgotPasswordModel(this.forgotform.get("email")?.value)

      this.accountService.forgotPassword(forgot).subscribe({
        next: (result) => {
          debugger
        },
        error: (err: { message: any; }) => alert(`Erro: ${err.message}`),
      });
    }
  }

  // Método para voltar à tela de login
  onBackToLogin() {
    this.router.navigate(['/account/login']);
  }
}
