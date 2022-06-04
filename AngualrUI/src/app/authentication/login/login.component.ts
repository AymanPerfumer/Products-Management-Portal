import { HttpErrorResponse } from '@angular/common/http';
import { AuthResponseDto } from './../../_interfaces/response/authResponseDto.model';
import { UserForAuthenticationDto } from './../../_interfaces/user/userForAuthenticationDto.model';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from './../../shared/services/authentication.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  private returnUrl: string | undefined;
  
  loginForm: FormGroup = new FormGroup({
    username: new FormControl("", [Validators.required]),
    password: new FormControl("", [Validators.required])
  });
  errorMessage: string = '';
  showError: boolean | undefined;

  constructor(private authService: AuthenticationService, private router: Router, private route: ActivatedRoute) { }
  
  ngOnInit(): void {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  validateControl = (controlName: string) => {
    return this.loginForm.get(controlName)?.invalid && this.loginForm.get(controlName)?.touched
  }

  hasError = (controlName: string, errorName: string) => {
    return this.loginForm.get(controlName)?.hasError(errorName)
  }
  
  loginUser = (loginFormValue: any) => {
    this.showError = false;
    const login = {... loginFormValue };

    const userForAuth: UserForAuthenticationDto = {
      username: login.username,
      password: login.password
    }

    this.authService.loginUser(userForAuth)
    .subscribe({
      next: (res:AuthResponseDto) => {
       localStorage.setItem("token", res.token);
       this.authService.sendAuthStateChangeNotification(true);
       this.router.navigate([this.returnUrl]);
    },
    error: (err: HttpErrorResponse) => {
      this.errorMessage = err.message;
      this.showError = true;
    }})
  }
}
