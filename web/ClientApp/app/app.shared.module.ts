import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { PratoComponent } from './components/prato/prato_consultar.component';
import { PratoCreateOrEditComponent } from './components/prato/prato_criar_editar.component';
import { RestauranteComponent } from './components/restaurante/restaurante_consultar.component';
import { RestauranteCreateOrEditComponent } from './components/restaurante/restaurante_criar_editar.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        PratoComponent,
        PratoCreateOrEditComponent,
        RestauranteComponent,
        RestauranteCreateOrEditComponent,
        HomeComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'pratos', component: PratoComponent },
            { path: 'prato/:id', component: PratoCreateOrEditComponent },
            { path: 'restaurantes', component: RestauranteComponent },
            { path: 'restaurante/:id', component: RestauranteCreateOrEditComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})

export class AppModuleShared {
}