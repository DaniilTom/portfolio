import { Component, Output, EventEmitter } from '@angular/core';
import { SightFilter } from '../../model/filters.model';
import { TypeService } from '../../data/types-data.service';
import { Type } from '../../model/base.model'

@Component({
    selector: 'filter-comp',
    templateUrl: './filter.component.html'
})
export class FilterComponent {

    filter: SightFilter = new SightFilter();
    types: Type[];

    constructor(public typeService: TypeService) {
        typeService.getTypes().then((data: Type[]) => this.types = data);
    }

    @Output() onApplyFilter = new EventEmitter<SightFilter>();

    applyFilter() {
        this.onApplyFilter.emit(this.filter);
    }
}
