import { YMapComponent } from './ymap.component';

export function ShowCollectionBehavior(yMapComp: YMapComponent) {
    // Определим свойства класса
    this.yMapComp = yMapComp;
    this.options = new window.ymaps.option.Manager(); // Менеджер опций
    this.events = new window.ymaps.event.Manager(); // Менеджер событий
}
// Определим методы.
ShowCollectionBehavior.prototype = {
    constructor: ShowCollectionBehavior,
    // Когда поведение будет включено, добавится событие щелчка на карту
    enable: function () {
        /*
        this._parent - родителем для поведения является менеджер поведений;
        this._parent.getMap() - получаем ссылку на карту;
        this._parent.getMap().events.add - добавляем слушатель события на карту.
        */
        this._parent.getMap().events.add('click', this._onClick, this);
        this._parent.getMap().events.add('boundschange', this._boundsOnChange, this);
    },
    disable: function () {
        this._parent.getMap().events.remove('click', this._onClick, this);
        this._parent.getMap().events.remove('boundschange', this._boundsOnChange, this);
    },
    // Устанавливает родителя для исходного поведения.
    setParent: function (parent) { this._parent = parent; },
    // Получает родителя
    getParent: function () { return this._parent; },
    // При щелчке на карте происходит ее центрирование по месту клика.
    _onClick: function (e) {
        //alert(this.coord.latitude);
        var coords = e.get('coords');
        this.yMapComp.setCoordinates(coords[0], coords[1]);
    },
    _boundsOnChange: function(e) {
        console.dir(e.get('newBounds'));
    }
}