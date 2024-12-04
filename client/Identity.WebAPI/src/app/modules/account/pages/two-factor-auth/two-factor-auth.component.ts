import { AfterViewInit, Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../../../../services/account.service';
import { TwoFactorAuthModel } from '../../model/TwoFactorAuthModel';
import { ValidateTwoFactorAuthRequest } from '../../model/ValidateTwoFactorAuthRequest';

@Component({
  selector: 'app-two-factor-auth',
  templateUrl: './two-factor-auth.component.html',
  styleUrl: './two-factor-auth.component.css'
})
export class TwoFactorAuthComponent implements OnInit, AfterViewInit {
  
  @ViewChildren('codeInput') codeInputs!: QueryList<any>; // Referência para os campos de entrada

  
  twoFactorForm: FormGroup;
  errorMessage: string = '';
  successMessage: string = '';
  valid = new TwoFactorAuthModel('teste@teste.com', 'Email');

  constructor(private fb: FormBuilder, private accountService: AccountService) {
    this.twoFactorForm = this.fb.group({
      code1: ['', [Validators.required]],
      code2: ['', [Validators.required]],
      code3: ['', [Validators.required]],
      code4: ['', [Validators.required]],
      code5: ['', [Validators.required]],
      code6: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    debugger
    this.accountService.sendTwoFactorCode(this.valid).subscribe({
      next: (result) => {
        debugger
      },
      error: (err: { message: any; }) => alert(`Erro: ${err.message}`),
    });
  }

  ngAfterViewInit(): void {
    // Chama a função para passar o foco para o próximo campo ao digitar um valor
    this.codeInputs.toArray().forEach((input, index) => {
      input.nativeElement.addEventListener('input', () => {
        const value = input.nativeElement.value;

        // Quando o campo de entrada recebe um valor, move o foco para o próximo input
        if (value.length === 1 && index < this.codeInputs.length - 1) {
          this.codeInputs.toArray()[index + 1].nativeElement.focus();
        } 
        // Quando o campo é apagado, move o foco para o campo anterior, mesmo que esteja vazio
        else if (value.length === 0 && index > 0) {
          this.codeInputs.toArray()[index - 1].nativeElement.focus();
        }
      });

      // Agora, vamos adicionar o evento para o "backspace" e mover o foco quando necessário
      input.nativeElement.addEventListener('keydown', (event: KeyboardEvent) => {
        if (event.key === 'Backspace' && input.nativeElement.value === '') {
          // Se o valor do campo estiver vazio e o usuário pressionar 'Backspace', vai para o campo anterior
          if (index > 0) {
            this.codeInputs.toArray()[index - 1].nativeElement.focus();
          }
        }
      });
    });
  }

  onSubmit(): void {
    debugger
    if (this.twoFactorForm.valid) {
      const data =  this.twoFactorForm.get('code1')?.value + this.twoFactorForm.get('code2')?.value + this.twoFactorForm.get('code3')?.value + this.twoFactorForm.get('code4')?.value + this.twoFactorForm.get('code5')?.value + this.twoFactorForm.get('code6')?.value;

      const valid = new ValidateTwoFactorAuthRequest(data, this.valid);

      this.accountService.validateTwoFactorCode(valid).subscribe({
        next: (result) => {
          debugger
          localStorage.setItem('authToken', result);
        },
        error: (err: { message: any; }) => alert(`Erro: ${err.message}`),
      });
    }
  }

  onFormSubmit(event:any)
  {
    debugger
    if (event) {

    const valid = new ValidateTwoFactorAuthRequest(event, this.valid);

    this.accountService.validateTwoFactorCode(valid).subscribe({
      next: (result) => {
        debugger
        localStorage.setItem('authToken', result);
      },
      error: (err: { message: any; }) => alert(`Erro: ${err.message}`),
    });
  }

  }
}