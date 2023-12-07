import { Component, Input, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

export type OptionsView = {
  question: string,
  btnYes: () => void,
  btnNot: () => void,
  redirect?: () => void,
}

@Component({
  selector: 'app-choose-preference',
  templateUrl: './choose-preference.component.html',
  styleUrls: ['./choose-preference.component.scss']
})
export class ChoosePreferenceComponent implements OnInit {
  @Input() public options: OptionsView[]
  public questionPosition = new BehaviorSubject(0);

  ngOnInit(): void {
    this.questionPosition.asObservable().subscribe(value => {
      const options = document.querySelectorAll<HTMLDivElement>('.option');
      options.forEach(e => {
        e.classList.add('hidden');

        if(e.className.includes(`option_${value}`)) {
          e.classList.remove('hidden');
        }
      })
    })
  }

  /**
   * function to change question to next question
   */
  public nextQuestion() {
    const previousValue = this.questionPosition.value;
    this.questionPosition.next(previousValue + 1)
  }

  /**
   * function to change question to previous question
   */
  public previousQuestion() {
    const previousValue = this.questionPosition.value;
    this.questionPosition.next(previousValue - 1)
  }
}
