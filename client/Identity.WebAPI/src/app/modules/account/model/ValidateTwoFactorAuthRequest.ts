import { TwoFactorAuthModel } from "./TwoFactorAuthModel";

export class ValidateTwoFactorAuthRequest {
    email: string;
    code: string;
    method: string; // "Email" ou "Phone"
  
    constructor(code:string, model:TwoFactorAuthModel) {
      this.email = model.email;
      this.code = code;
      this.method = model.method;
    }
  }
  