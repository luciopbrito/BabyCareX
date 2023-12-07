import { LocalStorageService } from 'src/app/services/localStorage/local-storage.service';
import { Component, OnInit } from '@angular/core';
import { Family } from 'src/app/interfaces/family.interface';
import { Schedule } from 'src/app/interfaces/schedule.interface';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  public hasSchedule: boolean;
  public schedules: Schedule[]
  constructor(private localStorageService: LocalStorageService) {}

  ngOnInit(): void {
    const account = this.localStorageService.getItem('login') as Family;

    this.hasSchedule = account.schedules.length > 0;
    if (this.hasSchedule) {
      this.schedules = account.schedules
    }
  }
}
