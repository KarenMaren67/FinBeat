import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SomethingRewriteComponent } from './something-rewrite/something-rewrite.component';
import { SomethingPagedComponent } from './something-paged/something-paged.component';

@NgModule({
  declarations: [
    AppComponent,
    SomethingRewriteComponent,
    SomethingPagedComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
