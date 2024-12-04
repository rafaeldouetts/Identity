import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountComponent } from './account.component';
import { RegisterComponent } from './pages/register/register.component';
import { LoginComponent } from './pages/login/login.component';
import { ForgotPasswordComponent } from './pages/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './pages/reset-password/reset-password.component';
import { ChangePasswordComponent } from './pages/change-password/change-password.component';
import { UpdateProfileComponent } from './pages/update-profile/update-profile.component';
import { ConfirmEmailComponent } from './pages/confirm-email/confirm-email.component';
import { ConfirmPhoneComponent } from './pages/confirm-phone/confirm-phone.component';
import { TwoFactorAuthComponent } from './pages/two-factor-auth/two-factor-auth.component';
import { AccountRoutingModule } from './account-routing.module';
import { AccountService } from '../../services/account.service';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { FactorAuthComponent } from './components/factor-auth/factor-auth.component';



@NgModule({
  declarations: [
    AccountComponent,
    RegisterComponent,
    LoginComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    ChangePasswordComponent,
    UpdateProfileComponent,
    ConfirmEmailComponent,
    ConfirmPhoneComponent,
    TwoFactorAuthComponent,
    FactorAuthComponent
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [
    AccountService  // Registre o serviço aqui se não usar 'providedIn: root'
  ]
})
export class AccountModule { }
