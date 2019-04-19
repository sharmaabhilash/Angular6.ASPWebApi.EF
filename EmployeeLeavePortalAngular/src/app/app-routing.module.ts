import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ApplyleaveComponent } from './applyleave/applyleave.component';
import { ViewleavesComponent } from './viewleaves/viewleaves.component';
import { GrantleaveComponent } from './grantleave/grantleave.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'applyleave', component: ApplyleaveComponent },
  { path: 'viewleaves', component: ViewleavesComponent },
  { path: 'grantleaves', component: GrantleaveComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
