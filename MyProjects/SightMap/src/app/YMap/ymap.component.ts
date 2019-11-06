import { Component, OnInit, Output, EventEmitter } from "@angular/core";
import ymaps from 'ymaps';

@Component({
    selector: 'yan-map',
    templateUrl: './ymap.component.html',
    styleUrls: ['./ymap.component.css']
})
export class YMapComponent implements OnInit {
    maps: any;
    myMap;

    myPlacemark: any;

    coordunates: Coordinates = new Coordinates(0, 0);

    @Output() coordinateChanged = new EventEmitter<Coordinates>();

    ngOnInit() {
        var bindFunc = this.setCoordinates.bind(this);
        ymaps.load().then(maps => {
            this.maps = maps;
            this.myMap = new maps.Map('map', {
                center: [59.939095, 30.315868],
                zoom: 7
            });
            this.myMap.events.add('click', function (e) {
                var coords = e.get('coords');
                bindFunc(coords[0], coords[1]);
            })
        }).catch(error => console.log('Failed to load Yandex Maps', error));
    }

    private setCoordinates(lt, lg) {
        this.coordunates.latitude = lt;
        this.coordunates.longitude = lg;
        this.coordinateChanged.emit(new Coordinates(lt, lg));

        this.myMap.geoObjects.remove(this.myPlacemark);
        this.myPlacemark = new this.maps.Placemark([lt, lg]);
        this.myMap.geoObjects.add(this.myPlacemark);
        
    }
}

export class Coordinates {
    constructor(
        public latitude: number,
        public longitude: number
    ) { }
}