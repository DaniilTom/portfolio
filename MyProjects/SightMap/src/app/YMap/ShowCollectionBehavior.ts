import { YMapComponent, Bounds, Coordinates } from './ymap.component';

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
        this._parent.getMap().events.add('boundschange', this._boundsOnChange, this);
    },
    disable: function () {
        this._parent.getMap().events.remove('boundschange', this._boundsOnChange, this);
    },
    // Устанавливает родителя для исходного поведения.
    setParent: function (parent) { this._parent = parent; },
    // Получает родителя
    getParent: function () { return this._parent; },
    // При щелчке на карте происходит ее центрирование по месту клика.
    _boundsOnChange: function(e) {
        var bounds = e.get('newBounds');
        var boundsDto = new Bounds(
            new Coordinates(bounds[0][0], bounds[0][1]),
            new Coordinates(bounds[1][0], bounds[1][1])
        );
        this.yMapComp.onBoundsChanged(boundsDto);
    }
}