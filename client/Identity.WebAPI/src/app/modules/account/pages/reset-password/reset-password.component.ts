import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../../../../services/account.service';
import { ActivatedRoute } from '@angular/router';
import { ResetPasswordModel } from '../../model/ResetPasswordModel';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css'
})
export class ResetPasswordComponent {
  changePasswordForm: FormGroup;
  loading = false;
  error = '';
  token: string | null = null;

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private route: ActivatedRoute

  ) {
    this.token = this.route.snapshot.paramMap.get('token'); // Acessando o parâmetro da URL
    console.log(this.token); // Token que foi passado via URL

    // Inicialize o formulário com validações
    this.changePasswordForm = this.fb.group({
      email: ['', [Validators.required]],
      newPassword: ['', [Validators.required, Validators.minLength(8)]],
    });
  }

  // Função chamada ao enviar o formulário
  onSubmit() {
    debugger
    // Verificar se os valores do formulário e o token estão presentes
const email = this.changePasswordForm.get("email")?.value;
const newPassword = this.changePasswordForm.get("newPassword")?.value;
const confirmPassword = this.changePasswordForm.get("confirmPassword")?.value;
const token = this.token; // Certifique-se de que o token foi atribuído corretamente

if(newPassword != confirmPassword)
{
  alert('A confirmação de senha não coincide.');
}

// Verificar se os valores não são nulos ou indefinidos
if (email && newPassword && token) {
  const reset = new ResetPasswordModel(email, newPassword, token);
  
  this.accountService.resetPassword(reset).subscribe({
    next: (result) => {
    },
    error: (err: { message: any; }) => alert(`Erro: ${err.message}`),
  });
  
} else {
  console.error('Os dados necessários não foram preenchidos corretamente.');
}
  }
}
