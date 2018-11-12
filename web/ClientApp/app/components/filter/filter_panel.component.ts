import { NgModule, Output, Input } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Text } from '@angular/compiler';
import { empty } from 'rxjs/Observer';
import 'rxjs/add/operator/map';
import { Core } from '../core/core';
import { Observable } from 'rxjs/Observable';

@Component({
    selector: 'filter_panel',
    templateUrl: './filter_panel.component.html'
})

export class FilterPanelComponent {
    private plainText: string;
    private filtered: boolean;

    constructor(public http: Http, @Inject('BASE_URL') public baseUrl: string) {
        this.plainText = "";
        this.filtered = false;      
    }      
}