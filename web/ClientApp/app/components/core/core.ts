import { Message } from "@angular/compiler/src/i18n/i18n_ast";

export module Core {
    export namespace Alerts {
        export class MessagePanel implements Interfaces.IMessagePanel {
            type: Enumerators.MessageType;
            className: string;

            constructor(public title?: string, public text?: string) {
                this.type = Enumerators.MessageType.none;
                this.className = "panel-primary";
            }

            hasMessage(): boolean {
                return !!this.text;
            }

            clear(): void {
                this.text = '';
                this.title = '';
            }

            success(text: string, title?: string): void {
                this.type = Enumerators.MessageType.success;
                this.className = "alert-success";
                this.title = !title ? "Operação realizada com sucesso" : title;
                this.text = !title ? "Sua ação foi realizada com sucesso" : text;
            }

            error(text: string, title?: string) {
                this.type = Enumerators.MessageType.error;
                this.className = "alert-danger";
                this.title = !title ? "Ocorreu um erro no sistema" : title;
                this.text = !title ? "Sua ação foi realizada com sucesso" : text;
            }

            warning(text: string, title?: string) {
                this.type = Enumerators.MessageType.warning;
                this.className = "alert-warning";
                this.title = !title ? "Aviso" : title;
                this.text = !title ? "Sua ação foi realizada com sucesso" : text;
            }
        }
    }

    export namespace Models {
        export class ModelBase implements Interfaces.IModelBase {
            constructor(
                public id?: number,
                public nome?: string
            )
            {              
                this.nome = !this.nome ? "" : this.nome;
            }

            editing(): boolean {
                return this.id != undefined && this.id > 0;
            }
        }
       
        export class Prato extends ModelBase implements Interfaces.IPrato {
            constructor(
                public valor?: number,
                public estabelecimentoId?: number,
                public nomeEstabelecimento?: string)
            {                
                super();
            }
        }

        export class Restaurante extends ModelBase implements Interfaces.IRestaurante {            
            constructor()
            {
                super();
            }

            editing(): boolean {
                return this.id != undefined && this.id > 0;
            }
        }
    }

    export namespace Interfaces {
        export interface IResponseModel<TModel> {
            ok: boolean;
            message: string;
            data: TModel;
        }

        export interface IModelBase {
            id?: number;
            nome?: string;
            editing(): boolean;
        }

        export interface IMessagePanel {
            title?: string;
            text?: string;
            hasMessage(): boolean;
            clear(): void;
            success(text?: string, title?: string) : void;
        }        

        export interface IPrato extends IModelBase {
            valor?: number;
            estabelecimentoId?: number;
            nomeEstabelecimento?: string;
        }

        export interface IRestaurante extends IModelBase  { }
    }

    export namespace Enumerators
    {
        export enum MessageType {
            none,
            success,
            error,
            warning
        }
    }
}