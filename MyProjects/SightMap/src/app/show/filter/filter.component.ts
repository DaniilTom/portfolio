import { Component, Output, EventEmitter, APP_INITIALIZER } from '@angular/core';
import { SightFilter } from '../../model/filters.model';
import { TypeService } from '../../data/types-data.service';
import { Type } from '../../model/base.model'
import { SightService } from 'src/app/data/sights-data.service';

@Component({
    selector: 'filter-comp',
    templateUrl: './filter.component.html'
})
export class FilterComponent {

    pageNum: number[] = [];
    pagesCount;
    currentPage: number;
    filter: SightFilter = new SightFilter();
    types: Type[];

    constructor(public typeService: TypeService, public sightService: SightService) {
        typeService.getTypes().then((data: Type[]) => this.types = data);
        this.getSigthsCount();
    }

    @Output() onApplyFilter = new EventEmitter<SightFilter>();

    applyFilter() {
        this.onApplyFilter.emit(this.filter);
        this.getSigthsCount();
    }

    async getSigthsCount() {
        var count = await this.sightService.getSightsCount(this.filter);
        this.pageNum = [];

        this.pagesCount = Math.ceil(count / this.filter.size);

        if (this.pagesCount < 4) {
            for (var i = 1; i <= this.pagesCount; i++) {
                this.pageNum.push(i);
            }
        }
        else {
            var begin: number;

            if (this.filter.offset == 1)
                begin = this.filter.offset;

            else if (this.filter.offset == this.pagesCount)
                begin = this.filter.offset - 2;

            else
                begin = this.filter.offset - 1;


            for (var i = begin; i < begin + 3; i++) {
                this.pageNum.push(i);
            }
        }
    }
}
