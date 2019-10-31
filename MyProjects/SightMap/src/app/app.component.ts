import { Component } from '@angular/core';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
})
export class AppComponent {

    renderCreate = false;
    renderShow = false;

    SwitchCreate() {
        this.renderCreate = !this.renderCreate;
        this.renderShow = false;
    }

    SwitchShow() {
        this.renderShow = !this.renderShow;
        this.renderCreate = false;
    }
}
