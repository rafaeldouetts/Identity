export class SendConfirmationTokenModel {
    email: string;
    phoneNumber: string;
  
    constructor(email:string, phone:string) {
      this.email = email;
      this.phoneNumber = phone;
    }
  }
  