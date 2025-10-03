import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent {

  currentYear: number = new Date().getFullYear();

  constructor() { }

  // Scroll to top
  scrollToTop(): void {
    window.scrollTo({
      top: 0,
      behavior: 'smooth'
    });
  }

  // Get footer links based on context
  getFooterLinks() {
    return {
      quickLinks: [
        { label: 'About Us', route: '/about' },
        { label: 'Contact', route: '/contact' },
        { label: 'Help & Support', route: '/help' },
        { label: 'Privacy Policy', route: '/privacy' },
        { label: 'Terms of Service', route: '/terms' }
      ],
      students: [
        { label: 'Courses', route: '/student/courses' },
        { label: 'Assignments', route: '/student/assignments' },
        { label: 'Mock Tests', route: '/student/tests' },
        { label: 'Job Opportunities', route: '/student/jobs' },
        { label: 'Resume Builder', route: '/student/resume' }
      ],
      institutions: [
        { label: 'Manage Courses', route: '/institution/courses' },
        { label: 'Student Progress', route: '/institution/students' },
        { label: 'Create Assignments', route: '/institution/assignments' },
        { label: 'Analytics', route: '/institution/reports' },
        { label: 'Settings', route: '/institution/settings' }
      ],
      companies: [
        { label: 'Post Jobs', route: '/industry/jobs' },
        { label: 'View Applications', route: '/industry/applications' },
        { label: 'Schedule Interviews', route: '/industry/interviews' },
        { label: 'Company Profile', route: '/industry/profile' },
        { label: 'Analytics', route: '/industry/analytics' }
      ]
    };
  }

  // Social media links
  getSocialLinks() {
    return [
      { name: 'Facebook', icon: 'fab fa-facebook-f', url: 'https://facebook.com' },
      { name: 'Twitter', icon: 'fab fa-twitter', url: 'https://twitter.com' },
      { name: 'LinkedIn', icon: 'fab fa-linkedin-in', url: 'https://linkedin.com' },
      { name: 'Instagram', icon: 'fab fa-instagram', url: 'https://instagram.com' }
    ];
  }
}