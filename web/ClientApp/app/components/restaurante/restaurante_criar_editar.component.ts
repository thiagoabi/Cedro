import { NgModule, OnInit, OnDestroy } from '@angular/core';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Text } from '@angular/compiler';
import { empty } from 'rxjs/Observer';
import { Core } from '../core/core';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { AppModule } from '../../app.server.module';
import { AppModuleShared } from '../../app.shared.module';
import { AppComponent } from '../app/app.component';


type IResponseModel<IPrato> = Core.Interfaces.IResponseModel<IPrato>;
type IRestaurante = Core.Interfaces.IRestaurante;
type MessageType = Core.Enumerators.MessageType;
type MessagePanel = Core.Alerts.MessagePanel;


@Component({
    selector: 'restaurante_criar_editar',
    templateUrl: './restaurante_criar_editar.component.html'
})

export class RestauranteCreateOrEditComponent implements OnInit, OnDestroy {
    public restaurante: IRestaurante;
    public restaurantes: IRestaurante[];
    public message: MessagePanel;
    private sub: any;

    constructor(public http: Http, @Inject('BASE_URL') public baseUrl: string, private route: ActivatedRoute) {
        this.restaurante = new Core.Models.Restaurante();
        this.message = new Core.Alerts.MessagePanel();
        this.restaurantes = [];
    }

    ngOnInit() {

        this.sub = this.route.params.subscribe(params => {
            this.getRestaurantes();
            this.restaurante.id = params['id'];
            if (this.restaurante.id != undefined && this.restaurante.id > 0) {
                this.filter();
            }
        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

    filter() {
        this.message.clear();
        this.http.get(this.baseUrl + 'api/restaurantes/' + this.restaurante.id).subscribe(result => {
            var response = JSON.parse((<any>result)._body) as IResponseModel<IRestaurante>;
            if (response.ok) {
                this.restaurante = response.data;
            } 
        }, error => console.error(error));
    }

    getRestaurantes() {
        this.message.clear();
        this.http.get(this.baseUrl + 'api/restaurantes/get?name=').subscribe(result => {
            var response = JSON.parse((<any>result)._body) as IResponseModel<IRestaurante[]>;
            if (response.ok) {
                this.restaurantes = response.data;
            }  
        }, error => console.error(error));
    }

    clear() {
        this.restaurante = new Core.Models.Restaurante();
    }

    save() {        
        var data = {
            Id: this.restaurante.id,
            Nome: this.restaurante.nome
        };
        this.message.clear();   
        this.http.post(this.baseUrl + 'api/restaurantes', data).subscribe(result => {
            var response = JSON.parse((<any>result)._body) as IResponseModel<IRestaurante>;
            if (result.ok) {
                this.restaurante = response.data;
                this.message.success("Registro " + (this.restaurante.id == 0 ? "Inserido" : "Alterado") + " com sucesso", "Mensagem do Sistema");
            } else {
                this.message.error(response.message);
            }
        }, error => console.error(error));
    }
}