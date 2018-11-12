import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppModuleShared } from './app.shared.module';
import { AppComponent } from './components/app/app.component';
import { Core } from './components/core/core';

@NgModule({
    bootstrap: [ AppComponent ],
    imports: [
        BrowserModule,
        AppModuleShared
    ],
    providers: [
        { provide: 'BASE_URL', useFactory: getBaseUrl }
    ]
})

export class AppModule {
    public Message: Core.Alerts.MessagePanel = new Core.Alerts.MessagePanel()
    public getBaseUrl() {

    }
}

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}