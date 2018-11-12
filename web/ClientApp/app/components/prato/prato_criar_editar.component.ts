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
type IPrato = Core.Interfaces.IPrato;
type Restaurante = Core.Models.Restaurante;
type IRestaurante = Core.Interfaces.IRestaurante;
type MessageType = Core.Enumerators.MessageType;
type MessagePanel = Core.Alerts.MessagePanel;


@Component({
    selector: 'prato_criar_editar',
    templateUrl: './prato_criar_editar.component.html'
})

export class PratoCreateOrEditComponent implements OnInit, OnDestroy {
    public prato: IPrato;
    public restaurantes: IRestaurante[];
    public message: MessagePanel;
    private sub: any;

    constructor(public http: Http, @Inject('BASE_URL') public baseUrl: string, private route: ActivatedRoute) {
        this.prato = new Core.Models.Prato();
        this.message = new Core.Alerts.MessagePanel();
        this.restaurantes = [];
    }

    ngOnInit() {

        this.sub = this.route.params.subscribe(params => {
            this.getRestaurantes();
            this.prato.id = params['id'];
            if (this.prato.id != undefined && this.prato.id > 0) {
                this.filter();
            }
        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

    filter() {
        this.message.clear();
        this.http.get(this.baseUrl + 'api/pratos/' + this.prato.id).subscribe(result => {
            var response = JSON.parse((<any>result)._body) as IResponseModel<IPrato>;
            if (response.ok) {
                this.prato = response.data;
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
        this.prato = new Core.Models.Prato();
    }

    save() {        
        var data = {
            Id: this.prato.id,
            Nome: this.prato.nome,
            Valor: this.prato.valor,
            EstabelecimentoId: this.prato.estabelecimentoId
        };
        this.message.clear();   
        this.http.post(this.baseUrl + 'api/pratos', data).subscribe(result => {
            var response = JSON.parse((<any>result)._body) as IResponseModel<IPrato>;
            if (result.ok) {
                this.prato = response.data;
                this.message.success("Registro " + (this.prato.id == 0 ? "Inserido" : "Alterado") + " com sucesso", "Mensagem do Sistema");
            } else {
                this.message.error(response.message);
            }            
        }, error => console.error(error));
    }
}