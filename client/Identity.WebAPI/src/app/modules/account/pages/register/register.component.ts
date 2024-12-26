import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from '../../../../services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerForm: FormGroup;
  isPasswordVisible: boolean = true; // Controle da visibilidade da senha

  constructor(private fb: FormBuilder, private accountService: AccountService, private router:Router) {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8), passwordValidator]],
      confirmPassword: ['', [Validators.required]],
      telefone: ['', [Validators.required, tamanhoTelefoneValidator, dddValidator]],
      dataNascimento: ['', [Validators.required]],
      nome: ['', [Validators.required, Validators.minLength(8)]],
    }, { validator: passwordMatchValidator });  // Aplica o validador ao FormGroup);
  }

  onSubmit(): void {
    debugger
    if (this.registerForm.valid) {
      this.accountService.register(this.registerForm.value).subscribe({
        next: () => this.router.navigate(['/account/login']),
        error: (err: { message: any; }) => alert(`Erro: ${err.message}`),
      });
    }
  }

    // Função para alternar a visibilidade da senha
    togglePasswordVisibility() {
      this.isPasswordVisible = !this.isPasswordVisible;
    }
}

export function passwordMatchValidator(control: FormGroup): ValidationErrors | null {
  const password = control.get('password')?.value;
  const confirmPassword = control.get('confirmPassword')?.value;

  // Se as senhas não coincidem, retorna um erro de validação
  if (password && confirmPassword && password !== confirmPassword) {
    return { passwordsMismatch: true };
  }
  return null;
}
// Método de validação customizada para senha
export function passwordValidator(control: AbstractControl): ValidationErrors | null {
  const password = control.value;

  debugger
  // Verifica se a senha possui pelo menos 8 caracteres
  if (password && password.length < 8) {
    return { minLength: 'A senha deve ter no mínimo 8 caracteres.' };
  }

  // Verifica se a senha contém pelo menos uma letra maiúscula
  if (!/[A-Z]/.test(password)) {
    return { uppercase: 'A senha deve conter pelo menos uma letra maiúscula.' };
  }

  // Verifica se a senha contém pelo menos uma letra minúscula
  if (!/[a-z]/.test(password)) {
    return { lowercase: 'A senha deve conter pelo menos uma letra minúscula.' };
  }

  // Verifica se a senha contém pelo menos um número
  if (!/\d/.test(password)) {
    return { number: 'A senha deve conter pelo menos um número.' };
  }

  // Verifica se a senha contém pelo menos um símbolo especial
  if (!/[@$!%*?&^]/.test(password)) {
    return { symbol: 'A senha deve conter pelo menos um símbolo especial (@, $, !, %, * ou &).' };
  }

  // Se passar em todas as verificações, a senha é válida
  return null;
}

// Validador para verificar o tamanho correto do telefone
export function tamanhoTelefoneValidator(control: AbstractControl): ValidationErrors | null {
  const telefone = control.value;

  if (!telefone) return null;

  // Remove qualquer formatação, como parênteses e espaços
  const telefoneLimpo = telefone.replace(/\D/g, '');

  // Verifica se o telefone tem entre 10 e 11 dígitos
  if (telefoneLimpo.length < 10 || telefoneLimpo.length > 11) {
    return { 'tamanhoInvalido': true };
  }

  return null; // Retorna null se o tamanho for válido
}

// Validador para o DDD
export function dddValidator(control: AbstractControl): ValidationErrors | null {
  const telefone = control.value;

  if (!telefone) return null;

  // Remove qualquer formatação, como parênteses e espaços
  const telefoneLimpo = telefone.replace(/\D/g, '');

  // Extrai o DDD (os dois primeiros dígitos)
  const ddd = telefoneLimpo.substring(0, 2);

  // Lista de DDDs válidos no Brasil
  const dddsValidos = [
    '11', '21', '31', '41', '51', '61', '71', '81', '91', '27', '28', '32', '33', '34', '35', '37', '38',
    '43', '44', '45', '46', '47', '48', '49', '53', '54', '55', '61', '62', '63', '64', '65', '66', '67', '68', 
    '69', '73', '74', '75', '76', '77', '78', '79', '81', '82', '83', '84', '85', '86', '87', '88', '89', '91', '92', 
    '93', '94', '95', '96', '97', '98', '99'
  ];

  // Verifica se o DDD está na lista de válidos
  if (!dddsValidos.includes(ddd)) {
    return { 'dddInvalido': true };
  }

  return null; // Retorna null se o DDD for válido
}
