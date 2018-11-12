import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Text } from '@angular/compiler';
import { empty } from 'rxjs/Observer';
import { Core } from '../core/core';

type IResponseModel<IPrato> = Core.Interfaces.IResponseModel<IPrato>;
type Prato = Core.Models.Prato;
type IPrato = Core.Interfaces.IPrato;
type MessageType = Core.Enumerators.MessageType;
type MessagePanel = Core.Alerts.MessagePanel;

@Component({
    selector: 'prato_consulta',
    templateUrl: './prato_consultar.component.html'
})

export class PratoComponent {
    public pratos: IPrato[];
    public plainText: string;
    public filtered: boolean;
    public message: MessagePanel;

    constructor(public http: Http, @Inject('BASE_URL') public baseUrl: string) {
        this.pratos = new Array();
        this.plainText = "";
        this.filtered = false;
        this.message = new Core.Alerts.MessagePanel();
        this.filter(); 
    }

    filter() {
        this.message.clear();
        this.filtered = !(!this.plainText);
        this.pratos = new Array();
        this.http.get(this.baseUrl + 'api/pratos/get?name=' + this.plainText).subscribe(result => {
            var response = JSON.parse((<any>result)._body) as IResponseModel<IPrato[]>;
            if (response.ok) {
                this.pratos = response.data;
            }
        }, error => console.error(error));
    }

    delete(id: number, index: number, nome: string) {        
        if (id > 0) {
            if (confirm("Deseja realmente excluir o " + nome + "?"))
                this.http.delete(this.baseUrl + 'api/pratos/delete?id=' + id).subscribe(result => {
                    var response = JSON.parse((<any>result)._body) as IResponseModel<IPrato>;
                    if (result.ok) {
                        this.pratos.splice(index, 1);
                        this.message.success("Registro excluido com sucesso", "Exclusao de Prato");
                    } else {
                        this.message.error(response.message);
                    }
            }, error => console.error(error));
        }
    }
}