import { Component, Input, OnInit, Output } from "@angular/core";
import { Album, State } from 'src/app/model/base.model';
import { Subject } from 'rxjs';
import { PluploadFile } from 'src/app/plupload/plupload.component';

@Component({
    selector: 'album-comp',
    templateUrl: './album.component.html',
    styleUrls: ['./album.component.css']
})
export class AlbumComponent implements OnInit {

    album: Album[];
    mainPage: Album;
    states = State;

    @Output() mainImageSet: Subject<void> = new Subject<void>();

    @Input() set setAlbum(value: Album[]) {
        this.album = value;
    }

    ngOnInit() {
        this.mainPage = this.album.find(page => page.isMain);
    }

    markMain(page: Album) {

        if (this.mainPage != undefined) {
            if (this.mainPage.state != State.Delete) {
                this.mainPage.isMain = false;
                this.mainPage.state = State.Edit;
            }
        }

        page.state = State.Edit;
        page.isMain = true;
        this.mainPage = page;

        this.mainImageSet.next();
    }

    changeName(page: Album, input: HTMLInputElement){
        page.title = input.value;
        page.state = State.Edit;
    }
}