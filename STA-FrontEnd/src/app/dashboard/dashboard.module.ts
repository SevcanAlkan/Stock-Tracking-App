import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';

import { DashboardLayoutComponent } from './components/dashboard-layout/dashboard-layout.component';
import { DashboardChartComponent } from './components/dashboard-chart/dashboard-chart.component';


@NgModule({
  declarations: [DashboardLayoutComponent, DashboardChartComponent],
  imports: [
    CommonModule,
    DashboardRoutingModule
  ]
})
export class DashboardModule { }
