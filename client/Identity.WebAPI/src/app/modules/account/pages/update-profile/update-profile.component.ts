import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../../../../services/account.service';

@Component({
  selector: 'app-update-profile',
  templateUrl: './update-profile.component.html',
  styleUrl: './update-profile.component.css'
})
export class UpdateProfileComponent {
  updateForm: FormGroup;
  
  constructor(private fb: FormBuilder, private accountService: AccountService) {
    this.updateForm = this.fb.group({
      nome: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      telefone: ['', Validators.required],
      dataNascimento: ['', Validators.required]
  });
}

  onSubmit(): void {
    debugger
    if (this.updateForm.valid) {
      // this.accountService.up(this.updateForm.value).subscribe({
      //   next: () => this.router.navigate(['/account/login']),
      //   error: (err: { message: any; }) => alert(`Erro: ${err.message}`),
      // });
    }
  }
}
