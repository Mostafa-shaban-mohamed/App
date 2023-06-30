import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable()
export class CanActivateGuard implements CanActivate {

  constructor(
    public router: Router
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const { userId, adminId } = route.queryParams;
    if (userId === adminId) {
      // this.router.navigate(['/admin']);
      return true; // This line allows user access to admin route
    }
    else {
      this.router.navigate(['/login']);
      return false;
    }
  }
}
