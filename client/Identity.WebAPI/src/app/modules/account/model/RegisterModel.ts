import { FormGroup, FormControl, Validators } from '@angular/forms';

export class RegisterModel {
  email: string;
  password: string;
  confirmPassword: string;
  telefone: string;
  dataNascimento: Date;
  nome: string;

  constructor() {
    this.email = '';
    this.password = '';
    this.confirmPassword = '';
    this.telefone = '';
    this.dataNascimento = new Date();
    this.nome = '';
  }

  static getFormGroup(): FormGroup {
    return new FormGroup({
      email: new FormControl('', [
        Validators.required,
        Validators.email
      ]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(/^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/)
      ]),
      confirmPassword: new FormControl('', [
        Validators.required,
        Validators.pattern(/^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/)
      ]),
      telefone: new FormControl('', [
        Validators.required,
        Validators.pattern(/^\(?\d{2}\)?\s?(\d{4,5})\s?-?\s?\d{4}$/)
      ]),
      dataNascimento: new FormControl('', [
        Validators.required,
        Validators.pattern(/\d{4}-\d{2}-\d{2}/)
      ]),
      nome: new FormControl('', [
        Validators.required,
        Validators.minLength(8)
      ])
    });
  }
}
