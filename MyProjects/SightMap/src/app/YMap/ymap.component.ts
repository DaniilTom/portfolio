import { Component, OnInit } from "@angular/core";
import ymaps from 'ymaps';

@Component({
    selector: 'yan-map',
    templateUrl: './ymap.component.html',
    styleUrls: ['./ymap.component.css']
})
export class YMapComponent implements OnInit {
    maps: any;
    myMap;

    lg: string;
    lt: string;

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

    private setCoordinates(lg, lt) {
        this.lg = lg;
        this.lt = lt;
    }
}