import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { APP_BASE_HREF, LocationStrategy, HashLocationStrategy } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ApplyleaveComponent } from './applyleave/applyleave.component';
import { ViewleavesComponent } from './viewleaves/viewleaves.component';
import { MenuesComponent } from './menues/menues.component';
import { GrantleaveComponent } from './grantleave/grantleave.component';
import { NgDatepickerModule } from 'ng2-datepicker';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    ApplyleaveComponent,
    ViewleavesComponent,
    MenuesComponent,
    GrantleaveComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    NgDatepickerModule
  ],
  providers: [{ provide: APP_BASE_HREF, useValue: '/employeeleaveportal' }, { provide: LocationStrategy, useClass: HashLocationStrategy }],
  bootstrap: [AppComponent]
})
export class AppModule { }
