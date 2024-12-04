export class TwoFactorAuthModel {
    email: string;
    method: string; // "Email" ou "Phone"
  
    constructor(email:string, method:string) {
      this.email = email;
      this.method = method;
    }
  }
  