
export function EditCreateBehavior(yMapComp) {
    this.yMapComp = yMapComp;
    this.options = new window.ymaps.option.Manager(); // Менеджер опций
    this.events = new window.ymaps.event.Manager(); // Менеджер событий
}

EditCreateBehavior.prototype = {
    constructor: EditCreateBehavior,

    enable: function () {
        // this._parent - родителем для поведения является менеджер поведений;
        this._parent.getMap().events.add('click', this._onClick, this);
    },
    disable: function () {
        this._parent.getMap().events.remove('click', this._onClick, this);
    },

    setParent: function (parent) { this._parent = parent; },

    getParent: function () { return this._parent; },
    
    _onClick: function (e) {
        var coords = e.get('coords');
        this.yMapComp.setCoordinates(coords[0], coords[1]);
    }
}