import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Text } from '@angular/compiler';
import { empty } from 'rxjs/Observer';
import { Core } from '../core/core';

type IResponseModel<IRestaurante> = Core.Interfaces.IResponseModel<IRestaurante>;
type IRestaurante = Core.Interfaces.IRestaurante;
type MessageType = Core.Enumerators.MessageType;
type MessagePanel = Core.Alerts.MessagePanel;

@Component({
    selector: 'restaurante_consulta',
    templateUrl: './restaurante_consultar.component.html'
})

export class RestauranteComponent {
    public restaurantes: IRestaurante[];
    public plainText: string;
    public filtered: boolean;
    public message: MessagePanel;

    constructor(public http: Http, @Inject('BASE_URL') public baseUrl: string) {
        this.restaurantes = new Array();
        this.plainText = "";
        this.filtered = false;
        this.message = new Core.Alerts.MessagePanel();
        this.filter(); 
    }

    filter() {
        this.message.clear();
        this.filtered = !(!this.plainText);
        this.restaurantes = new Array();
        this.http.get(this.baseUrl + 'api/restaurantes/get?name=' + this.plainText).subscribe(result => {
            var response = JSON.parse((<any>result)._body) as IResponseModel<IRestaurante[]>;
            if (response.ok) {
                this.restaurantes = response.data;
            }
        }, error => console.error(error));
    }

    delete(id: number, index: number, nome: string) {        
        if (id > 0) {
            if (confirm("Deseja realmente excluir o " + nome + " e todos os pratos cadastrados dele?"))
                this.http.delete(this.baseUrl + 'api/restaurantes/delete?id=' + id).subscribe(result => {
                    var response = JSON.parse((<any>result)._body) as IResponseModel<IRestaurante>;
                    if (result.ok) {
                        this.restaurantes.splice(index, 1);
                        this.message.success("Registro excluido com sucesso", "Exclusao de Restaurante");
                    } else {
                        this.message.error(response.message);
                    }
            }, error => console.error(error));
        }
    }
}