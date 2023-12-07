import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Baba } from 'src/app/interfaces/baba.interface';
import { Schedule } from 'src/app/interfaces/schedule.interface';

@Component({
  selector: 'app-baba-info',
  templateUrl: './baba-info.component.html',
  styleUrls: ['./baba-info.component.scss']
})
export class BabaInfoComponent implements OnInit{
  @Input() public id: number;
  @Input() public scheduleId?: number;
  @Input() public forFamily: boolean;

  public baba$: Observable<Baba>;
  public schedule$: Observable<Schedule>;
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.baba$ = this.http.get<Baba>(`https://localhost:7010/api/Babas/${this.id}`)
    this.schedule$ =  this.http.get<Schedule>(`https://localhost:7010/api/Schedules/${this.scheduleId}`)
  }
}
