import { Component, Output, EventEmitter } from '@angular/core';
import { SightFilter } from '../../model/filters.model';
import { DataService } from '../../data/data.service';

@Component({
    selector: 'filter-comp',
    templateUrl: './filter.component.html'
})
export class FilterComponent {

    filter: SightFilter = new SightFilter();

    constructor(public dataService: DataService) { }

    @Output() onApplyFilter = new EventEmitter<SightFilter>();

    applyFilter() {
        this.onApplyFilter.emit(this.filter);
    }
}
