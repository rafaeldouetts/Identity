export class ResetPasswordModel {
    email: string;
    newPassword: string;
    token: string;
  
    constructor(email:string, password:string, token:string) {
      this.email = email;
      this.newPassword = password;
      this.token = token;
    }
  }
  