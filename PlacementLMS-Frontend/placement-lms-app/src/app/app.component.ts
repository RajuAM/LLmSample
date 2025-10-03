import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'PlacementLMS - Placement Cell Learning Management System';
  showHeader = true;
  showFooter = true;

  constructor(private router: Router) {}

  ngOnInit(): void {
    // Listen to route changes to control header/footer visibility
    this.router.events.pipe(
      filter((event): event is NavigationEnd => event instanceof NavigationEnd)
    ).subscribe((event: NavigationEnd) => {
      this.updateLayoutVisibility(event.url);
    });

    // Set initial visibility
    this.updateLayoutVisibility(this.router.url);
  }

  private updateLayoutVisibility(url: string): void {
    // Hide header and footer on auth pages
    const authRoutes = ['/auth/login', '/auth/register', '/auth/forgot-password'];
    const isAuthPage = authRoutes.some(route => url.startsWith(route));

    this.showHeader = !isAuthPage;
    this.showFooter = !isAuthPage;
  }
}
