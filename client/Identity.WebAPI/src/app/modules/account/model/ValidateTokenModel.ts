export class ValidateTokenModel {
    token: string;
    email: string;
    phoneNumber: string;
  
    constructor(token:string, email:string, phone:string) {
      this.token = token;
      this.email = email;
      this.phoneNumber = phone;
    }
  }
  