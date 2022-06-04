import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from './../shared/services/authentication.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  isCollapsed: boolean = false;
  isUserAuthenticated: boolean | undefined;
  
  constructor(private authService: AuthenticationService, private router: Router) {
    this.isUserAuthenticated = localStorage.getItem("token") != null;
  }

  ngOnInit(): void {
    this.authService.authChanged
    .subscribe(res => {
      this.isUserAuthenticated = res;
    })
  }

  public logout = () => {
    this.authService.logout();
    this.router.navigate(["/"]);
  }

}
