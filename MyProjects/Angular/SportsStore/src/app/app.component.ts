import { Component } from '@angular/core';

@Component({
  selector: 'app',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  //template: `<div class="bg-success p-a-1 text-xs-centre">This is sportsstore</div>`
})
export class AppComponent {
  title = 'app works!';
  products = [{id: 1}, {id: 2}];
}
