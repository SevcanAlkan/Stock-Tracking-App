import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardLayoutComponent } from './components/dashboard-layout/dashboard-layout.component';
import { DashboardChartComponent } from './components/dashboard-chart/dashboard-chart.component';



@NgModule({
  declarations: [DashboardLayoutComponent, DashboardChartComponent],
  imports: [
    CommonModule
  ]
})
export class DasboardModule { }
