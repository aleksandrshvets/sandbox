import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { QtaComponent } from './qta/qta.component';

const routes: Routes = [
  { path: 'qta', component: QtaComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})


export class AppRoutingModule { }
