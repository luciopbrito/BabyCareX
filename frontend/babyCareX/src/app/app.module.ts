import { FamilyModule } from './pages/family/family.module';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HeaderComponent } from './components/base/header/header.component';
import { FooterComponent } from './components/base/footer/footer.component';
import { BaseStructureComponent } from './components/base/base-structure/base-structure.component';
import { ErrorRouteModule } from './pages/error-route/error-route.module';
import { PreferenceModule } from './pages/preference/preference.module';

@NgModule({
  declarations: [AppComponent, HeaderComponent, FooterComponent, BaseStructureComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FamilyModule,
    ErrorRouteModule,
    PreferenceModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
