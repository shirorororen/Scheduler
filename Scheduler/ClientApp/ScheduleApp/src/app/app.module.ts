import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component'; 
import { jqxSchedulerModule } from 'jqwidgets-ng/jqxscheduler';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule, jqxSchedulerModule, HttpClientModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }