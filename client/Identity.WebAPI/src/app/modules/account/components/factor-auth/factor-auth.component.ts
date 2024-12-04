import { AfterViewInit, Component, EventEmitter, OnInit, Output, QueryList, ViewChildren } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-factor-auth',
  templateUrl: './factor-auth.component.html',
  styleUrl: './factor-auth.component.css'
})
export class FactorAuthComponent implements OnInit, AfterViewInit {
  twoFactorForm: FormGroup;
  errorMessage!: string;
  successMessage!: string;

   // Emite um evento quando o formulário é enviado
  @Output() formSubmit: EventEmitter<string> = new EventEmitter<string>();

  @ViewChildren('codeInput') codeInputs!: QueryList<any>; // Referência para os campos de entrada

  
  
  constructor(private fb: FormBuilder) {
    // Criação do formulário com as validações necessárias
    this.twoFactorForm = this.fb.group({
      code1: ['', [Validators.required, Validators.maxLength(1)]],
      code2: ['', [Validators.required, Validators.maxLength(1)]],
      code3: ['', [Validators.required, Validators.maxLength(1)]],
      code4: ['', [Validators.required, Validators.maxLength(1)]],
      code5: ['', [Validators.required, Validators.maxLength(1)]],
      code6: ['', [Validators.required, Validators.maxLength(1)]]
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.twoFactorForm.valid) {
      // Coleta os valores do formulário e junta para formar o código
      const code = Object.values(this.twoFactorForm.value).join('');
      
      // Dispara o evento com o código de autenticação
      this.formSubmit.emit(code);

      // Mensagens de sucesso e erro
      this.successMessage = `Código enviado: ${code}`;
      this.errorMessage = '';  // Resetando mensagem de erro
    } else {
      this.errorMessage = 'Por favor, insira todos os campos corretamente.';
      this.successMessage = '';  // Resetando mensagem de sucesso
    }
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

  onPaste(event: ClipboardEvent): void {
    // Impede o comportamento padrão do navegador
    event.preventDefault();
  
    // Obter o conteúdo do clipboard
    const pastedText = event.clipboardData?.getData('text');
    console.log('Conteúdo colado:', pastedText); // Exibe o conteúdo no console
  
    if(!pastedText) return;

    // Verifica se o conteúdo colado é válido (somente números neste caso)
    if (!/^\d{6}$/.test(pastedText)) {
      console.error('Conteúdo inválido. Apenas números são permitidos.');
      return;
    }
  
    // Obtém todos os campos de entrada
    const inputs = Array.from(document.querySelectorAll('input[type="text"]')) as HTMLInputElement[];
  
    // Preenche os inputs com os valores colados
    for (let i = 0; i < pastedText.length; i++) {
      if (inputs[i]) {
        inputs[i].value = pastedText[i]; // Preenche o valor de cada input com o caractere correspondente
      }
    }
  
    // Coloca o foco no próximo campo de entrada (se houver)
    const nextInput = inputs[pastedText.length];
    if (nextInput) {
      nextInput.focus();
    }
  }
  
}