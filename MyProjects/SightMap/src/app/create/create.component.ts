import { Component, ViewChild, TemplateRef, ViewContainerRef } from "@angular/core";
import { Sight, Type, Album } from '../model/base.model';
import { NgForm } from '@angular/forms';
import { TypeService } from '../data/types-data.service';
import { SightService } from '../data/sights-data.service';
import { Coordinates } from '../YMap/ymap.component';
import { Subject } from 'rxjs';
import { templateJitUrl } from '@angular/compiler';

@Component(
    {
        selector: 'create-comp',
        templateUrl: './create.component.html',
        styleUrls: ['./create.component.css']
    })
export class CreateComponent {

    removeInitPoint: Subject<void> = new Subject<void>();

    newSight: Sight = new Sight();
    newType: Type = new Type();

    album: AlbumTempTemp[] = [];

    mainImage: AlbumTemp = new AlbumTemp();

    mainImgDiv: HTMLDivElement;

    types: Type[];

    @ViewChild('imgInput', {read: TemplateRef, static: false}) imgInput: TemplateRef<any>;
    @ViewChild('container', {read: ViewContainerRef, static: false}) container: ViewContainerRef;

    constructor(public sightService: SightService, public typeService: TypeService) {
        this.newSight.type = new Type();
        typeService.getTypes().then((data: Type[]) => this.types = data);
    }

    testMethod($event, input) {
        console.dir($event);
        console.dir(input);
    }

    addSight(ngform: NgForm) {
        if (ngform.valid) {
            var form = document.forms.namedItem('createSight');
            var formData = new FormData(form);
            this.sightService.addSight(formData).then((data: Sight) => {
                if (data != null)
                    alert("Добавлено с id:" + data.id);
                ngform.resetForm();
                this.removeInitPoint.next();
            });

        }
        else
            alert("Ошибки в форме.")
    }

    addType(ngform: NgForm) {
        if (ngform.valid) {
            var form = document.forms.namedItem('createType');
            var formData = new FormData(form);
            this.typeService.addType(formData).then((data: Type) => {
                if (data != null) {
                    alert("Добавлено с id:" + data.id);
                    this.types.push(data);
                }
            });
        }
        else
            alert("Ошибки в форме.")
    }

    // метод подписки на событие карты coordinateChanged
    setCoordinates(coord: Coordinates) {
        this.newSight.latitude = coord.latitude;
        this.newSight.longitude = coord.longitude;
    }

    open(event: MouseEvent) {
        console.dir(event);
    }

    changeImage(div: HTMLDivElement){
        var input = div.getElementsByClassName("file")[0] as HTMLInputElement;
        var img = div.getElementsByTagName("img")[0] as HTMLImageElement;
        
        if (input.files[0] == undefined) {
            img.src = "";
            return;
        }

        var reader = new FileReader();
        reader.onloadend = function (e) {
            img.src = reader.result.toString();
        }

        reader.readAsDataURL(input.files[0]);
    }

    addNewImage(div: HTMLDivElement, isMain?: boolean) {
        this.changeImage(div);

        var t = this.container.createEmbeddedView(this.imgInput);
        
        console.dir(t);
    }

    swapDiv(div: HTMLDivElement, $event) {
        var mainParent = this.mainImgDiv.parentNode;
        var childParent = div.parentNode;

        this.mainImgDiv.classList.remove("main-preview");
        //(this.mainImgDiv.getElementsByClassName("file")[0] as HTMLInputElement).name = "secondary";

        div.classList.add("main-preview");
        //(div.getElementsByClassName("file")[0] as HTMLInputElement).name = "main";

        mainParent.replaceChild(div, this.mainImgDiv);
        childParent.prepend(this.mainImgDiv);

        this.mainImgDiv = div;

        $event.preventDefault();
    }
}

export class AlbumTemp {
    public inputElement: HTMLInputElement;
    public imageElement: HTMLImageElement;
}

export class AlbumTempTemp{
    public imgAsUrl: string;
    public imageName: string;
}
