import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../../../services/account.service';
import { ValidateTokenModel } from '../../model/ValidateTokenModel';
import { SendConfirmationTokenModel } from '../../model/SendConfirmationTokenModel';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrl: './confirm-email.component.css'
})
export class ConfirmEmailComponent implements OnInit {

  send?:SendConfirmationTokenModel = new SendConfirmationTokenModel("teste@teste.com", "");
  token: string | null = null;

  constructor(private accountService: AccountService, private route: ActivatedRoute) {
    
  }

  ngOnInit(): void {
    this.token = this.route.snapshot.paramMap.get('token');

    if (this.token) {

      var validate = new ValidateTokenModel(this.token, "teste@teste.com", "");

      this.accountService.validateEmailToken(validate).subscribe({
        next: (result) => {
        },
        error: (err: { message: any; }) => alert(`Erro: ${err.message}`),
      });
    }
    else if(this.send)
    {
      this.accountService.sendEmailConfirmation(this.send).subscribe({
        next: (result) => {
        },
        error: (err: { message: any; }) => alert(`Erro: ${err.message}`),
      });
    }
  }
}
