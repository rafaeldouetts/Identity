import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../../../../services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
 loginForm: FormGroup;

 constructor(private fb: FormBuilder, private accountService: AccountService,  private router:Router) {
  this.loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
  });
}

  onSubmit(): void {
    debugger
    if (this.loginForm.valid) {
      this.accountService.login(this.loginForm.value).subscribe({
        next: (result) => {
          debugger
          this.accountService.setToken(result);
          this.router.navigate(['/account/two-factor-auth']);
        },
        error: (err: { message: any; }) => alert(`Erro: ${err.message}`),
      });
    }
  }
}
