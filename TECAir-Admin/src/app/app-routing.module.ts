import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login/login.component';
import { TecairlinesComponent } from './components/tecairlines/tecairlines/tecairlines.component';
import { HomeComponent } from './components/tecairlines/home/home.component';
import { FlightsComponent } from './components/tecairlines/flights/flights/flights.component';
import { AddFlightComponent } from './components/tecairlines/flights/add-flight/add-flight.component';
import { EditFlightComponent } from './components/tecairlines/flights/edit-flight/edit-flight.component';
import { TripsComponent } from './components/tecairlines/trips/trips/trips.component';
import { AddTripComponent } from './components/tecairlines/trips/add-trip/add-trip.component';
import { CheckBoardingpassBaggagesComponent } from './components/tecairlines/check-boardingpass-baggages/check-boardingpass-baggages.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo:'tecair-admin/login' },
  { path: 'tecair-admin/login', component: LoginComponent },
  {
    path: 'tecair-admin/:id',
    component: TecairlinesComponent,
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'home' },
      { path: 'home', component: HomeComponent },
      { path: 'trips', component: TripsComponent },
      { path: 'add-trip', component: AddTripComponent },
      { path: 'flights', component: FlightsComponent },
      { path: 'add-flight', component: AddFlightComponent },
      { path: 'edit-flight/:id', component: EditFlightComponent },
      { path: 'chack-boardingpass-baggages', component: CheckBoardingpassBaggagesComponent }
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
