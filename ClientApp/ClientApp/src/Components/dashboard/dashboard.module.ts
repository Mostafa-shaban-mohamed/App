import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PieChartComponent } from './pie-chart/pie-chart.component';
import { DashboardComponent } from './dashboard.component';
import { FormsModule } from '@angular/forms';
import { NgApexchartsModule } from 'ng-apexcharts';
import { LineChartComponent } from './line-chart/line-chart.component';



@NgModule({
  declarations: [
    PieChartComponent,
    DashboardComponent,
    LineChartComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    NgApexchartsModule
  ]
})
export class DashboardModule { }
