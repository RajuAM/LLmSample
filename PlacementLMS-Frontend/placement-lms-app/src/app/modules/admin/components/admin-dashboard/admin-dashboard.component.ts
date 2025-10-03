import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../../../core/services/admin.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  dashboardStats = {
    totalUsers: 0,
    totalRoles: 0,
    totalPermissions: 0,
    totalCourseGroups: 0
  };

  recentActivities: any[] = [];

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    this.loadDashboardStats();
  }

  loadDashboardStats(): void {
    // Load users count
    this.adminService.getAllUsers().subscribe(users => {
      this.dashboardStats.totalUsers = users.length;
    });

    // Load roles count
    this.adminService.getAllRoles().subscribe(roles => {
      this.dashboardStats.totalRoles = roles.length;
    });

    // Load permissions count
    this.adminService.getAllPermissions().subscribe(permissions => {
      this.dashboardStats.totalPermissions = permissions.length;
    });

    // Load course groups count
    this.adminService.getAllCourseGroups().subscribe(courseGroups => {
      this.dashboardStats.totalCourseGroups = courseGroups.length;
    });
  }
}