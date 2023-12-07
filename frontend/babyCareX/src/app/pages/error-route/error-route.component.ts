import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-error-route',
  templateUrl: './error-route.component.html',
  styleUrls: ['./error-route.component.scss']
})
export class ErrorRouteComponent implements OnInit {
  public message = '';

  constructor(
    private route: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe(e => {
      this.message = e['message'] ? e['message'] : 'Página não encontrada'
    })
  }
}
