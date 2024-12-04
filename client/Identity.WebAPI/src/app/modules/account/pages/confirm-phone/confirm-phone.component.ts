import { Component, OnInit } from '@angular/core';
import { ValidateTokenModel } from '../../model/ValidateTokenModel';
import { AccountService } from '../../../../services/account.service';
import { ActivatedRoute } from '@angular/router';
import { SendConfirmationTokenModel } from '../../model/SendConfirmationTokenModel';

@Component({
  selector: 'app-confirm-phone',
  templateUrl: './confirm-phone.component.html',
  styleUrl: './confirm-phone.component.css'
})
export class ConfirmPhoneComponent implements OnInit {

  send?:SendConfirmationTokenModel = new SendConfirmationTokenModel("", "11956478552");
  token: string | null = null;

  constructor(private accountService: AccountService) {
    
  }

  ngOnInit(): void {
    
    if(this.send)
    {
      this.accountService.sendPhoneConfirmation(this.send).subscribe({
        next: (result) => {
        },
        error: (err: { message: any; }) => alert(`Erro: ${err.message}`),
      });
    }
  }

  onFormSubmit(event:any)
  {
    debugger
    if (event) {

    const valid = new ValidateTokenModel(event, "", "11956478552");

    this.accountService.validatePhoneToken(valid).subscribe({
      next: (result) => {
        debugger
        localStorage.setItem('authToken', result);
      },
      error: (err: { message: any; }) => alert(`Erro: ${err.message}`),
    });
  }

  }
}
  