import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/auth/auth.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {

  form: FormGroup;

  @Input() error: string | null;

  constructor(private fb:FormBuilder, 
    private authService: AuthService, 
    private router: Router) { 
      this.form = this.fb.group({
        email: ['',Validators.required],
        password: ['',Validators.required]
    });
  }

  ngOnInit(): void {


  }

  login() {
    const val = this.form.value;

    if (val.email && val.password) {
        this.authService.login(val.email, val.password)
            .subscribe(
                () => {
                    console.log("User is logged in");
                    this.router.navigateByUrl('/');
                },
                (error: any) => {
                  console.log(error);
                }
            );
    }
  }

}
