import { YMapComponent, Bounds, Coordinates } from './ymap.component';

export function ShowCollectionBehavior(yMapComp: YMapComponent) {
    this.yMapComp = yMapComp;
    this.options = new window.ymaps.option.Manager(); // Менеджер опций
    this.events = new window.ymaps.event.Manager(); // Менеджер событий
}

ShowCollectionBehavior.prototype = {
    constructor: ShowCollectionBehavior,
    enable: function () {
        // this._parent - родителем для поведения является менеджер поведений;
        this._parent.getMap().events.add('boundschange', this._boundsOnChange, this);
    },
    disable: function () {
        this._parent.getMap().events.remove('boundschange', this._boundsOnChange, this);
    },

    setParent: function (parent) { this._parent = parent; },

    getParent: function () { return this._parent; },

    _boundsOnChange: function(e) {
        var bounds = e.get('newBounds');
        var boundsDto = new Bounds(
            new Coordinates(bounds[0][0], bounds[0][1]),
            new Coordinates(bounds[1][0], bounds[1][1])
        );
        this.yMapComp.onBoundsChanged(boundsDto);
    }
}