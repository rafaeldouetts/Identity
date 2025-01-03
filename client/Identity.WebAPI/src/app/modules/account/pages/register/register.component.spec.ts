// import { ComponentFixture, TestBed } from '@angular/core/testing';
// import { RegisterComponent } from './register.component';
// import { FormsModule } from '@angular/forms'; // Para testar formulários
// import { HttpClientTestingModule } from '@angular/common/http/testing';
// import { HttpClient } from '@angular/common/http';
// import { AccountService } from '../../../../services/account.service';

// describe('RegisterComponent', () => {
//   let component: RegisterComponent;
//   let fixture: ComponentFixture<RegisterComponent>;

//   beforeEach(async () => {
//     await TestBed.configureTestingModule({
//       declarations: [RegisterComponent],
//       imports: [FormsModule, HttpClientTestingModule], // Importa o módulo FormsModule para testar formulários
//       providers: [AccountService]
//     })
//     .compileComponents();

//     fixture = TestBed.createComponent(RegisterComponent);
//     component = fixture.componentInstance;
//     fixture.detectChanges();
//   });

//   it('should create', () => {
//     expect(component).toBeTruthy();
//   });

//   it('should have a form with an email and password input', () => {
//     const emailInput = fixture.debugElement.nativeElement.querySelector('input[type="email"]');
//     const passwordInput = fixture.debugElement.nativeElement.querySelector('input[type="password"]');
    
//     expect(emailInput).toBeTruthy();
//     expect(passwordInput).toBeTruthy();
//   });

//   it('should call onSubmit when the form is submitted', () => {
//     const onSubmitSpy = spyOn(component, 'onSubmit');
//     const form = fixture.debugElement.nativeElement.querySelector('form');
//     form.submit();

//     expect(onSubmitSpy).toHaveBeenCalled();
//   });

//   it('should show an error message if the form is invalid', () => {
//     component.registerForm.controls['email'].setValue('');
//     component.registerForm.controls['password'].setValue('');
//     fixture.detectChanges();

//     const errorMessage = fixture.debugElement.nativeElement.querySelector('.error-message');
//     expect(errorMessage).toBeTruthy();
//   });

//   it('should show a success message if the form is valid', () => {
//     component.registerForm.controls['email'].setValue('test@example.com');
//     component.registerForm.controls['password'].setValue('password123');
//     fixture.detectChanges();

//     const successMessage = fixture.debugElement.nativeElement.querySelector('.success-message');
//     expect(successMessage).toBeTruthy();
//   });
// });
