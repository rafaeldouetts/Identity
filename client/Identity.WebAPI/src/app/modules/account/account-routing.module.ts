import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './pages/register/register.component';
import { LoginComponent } from './pages/login/login.component';
import { ForgotPasswordComponent } from './pages/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './pages/reset-password/reset-password.component';
import { ChangePasswordComponent } from './pages/change-password/change-password.component';
import { UpdateProfileComponent } from './pages/update-profile/update-profile.component';
import { ConfirmEmailComponent } from './pages/confirm-email/confirm-email.component';
import { ConfirmPhoneComponent } from './pages/confirm-phone/confirm-phone.component';
import { TwoFactorAuthComponent } from './pages/two-factor-auth/two-factor-auth.component';

const routes: Routes = [
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'reset-password/:token', component: ResetPasswordComponent },
  { path: 'change-password', component: ChangePasswordComponent },
  { path: 'update-profile', component: UpdateProfileComponent },
  { path: 'confirm-email', component: ConfirmEmailComponent },
  { path: 'confirm-email/:token', component: ConfirmEmailComponent },
  { path: 'confirm-phone', component: ConfirmPhoneComponent },
  { path: 'two-factor-auth', component: TwoFactorAuthComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)], // Importa o RouterModule com as rotas
  exports: [RouterModule], // Exporta o RouterModule para ser usado no AccountModule
})
export class AccountRoutingModule {}
