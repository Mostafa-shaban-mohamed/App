import { Component, ViewChild } from '@angular/core';
import {
  ChartComponent,
  ApexNonAxisChartSeries,
  ApexResponsive,
  ApexChart
} from "ng-apexcharts";
import { DashboardService } from '../../../shared/services/dashboard.service';

export type ChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  responsive: ApexResponsive[];
  labels: any;
};

@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css']
})
export class PieChartComponent {
  @ViewChild("chart")
    chart!: ChartComponent;
  public chartOptions: Partial<ChartOptions> | any;

  constructor(private service: DashboardService) {
    this.service.pieChart().subscribe((resp) => {
      var data = JSON.stringify(resp);
      var resposne = JSON.parse(data);
      this.chartOptions = {
        series: resposne.series,//[44, 55, 13, 43, 22],
        chart: {
          width: 380,
          type: "donut"
        },
        labels: resposne.labels,//["Team A", "Team B", "Team C", "Team D", "Team E"],
        responsive: [
          {
            breakpoint: 480,
            options: {
              chart: {
                width: 200
              },
              legend: {
                position: "bottom"
              }
            }
          }
        ]
      };
    });
  }
}
