import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HeaderComponent } from './components/base/header/header.component';
import { SharedModule } from './shared/shared.module';
import { FooterComponent } from './components/base/footer/footer.component';
import { ErrorRouteComponent } from './components/base/error-route/error-route.component';
import { BaseStructureComponent } from './components/base/base-structure/base-structure.component';

@NgModule({
  declarations: [AppComponent, HeaderComponent, FooterComponent, ErrorRouteComponent, BaseStructureComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    SharedModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
