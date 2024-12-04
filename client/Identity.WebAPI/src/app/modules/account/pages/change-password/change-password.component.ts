import { Component } from '@angular/core';
import { AccountService } from '../../../../services/account.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ChangePasswordModel } from '../../model/ChangePasswordModel';
import { ActivatedRoute } from '@angular/router';
import { ResetPasswordModel } from '../../model/ResetPasswordModel';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.css'
})
export class ChangePasswordComponent {
  changePasswordForm: FormGroup;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private route: ActivatedRoute

  ) {
    // Inicialize o formulário com validações
    this.changePasswordForm = this.fb.group({
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmNewPassword: ['', [Validators.required, Validators.minLength(8)]],
      newPassword: ['', [Validators.required, Validators.minLength(8)]],
    });
  }
  onSubmit() {
    debugger
  const password = this.changePasswordForm.get("password")?.value;
  const newPassword = this.changePasswordForm.get("newPassword")?.value;
  const confirmPassword = this.changePasswordForm.get("confirmNewPassword")?.value;


if(newPassword != confirmPassword)
{
  alert('A confirmação de senha não coincide.');
}

// Verificar se os valores não são nulos ou indefinidos
if (newPassword && password) {
  const reset = new ChangePasswordModel(password, newPassword);
  
  this.accountService.changePassword(reset).subscribe({
    next: (result) => {
    },
    error: (err: { message: any; }) => alert(`Erro: ${err.message}`),
  });
  
} else {
  console.error('Os dados necessários não foram preenchidos corretamente.');
}
  }
}

