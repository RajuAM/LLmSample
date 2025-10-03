import { Injectable } from '@angular/core';

// Note: To use this service with actual charts, install chart.js and ng2-charts:
// npm install chart.js ng2-charts

@Injectable({
  providedIn: 'root'
})
export class ChartService {

  constructor() { }

  // Chart Color Schemes
  getColorSchemes() {
    return {
      primary: ['#007bff', '#28a745', '#ffc107', '#fd7e14', '#dc3545', '#6f42c1', '#e83e8c', '#20c997'],
      blue: ['#007bff', '#0056b3', '#004085', '#cce7ff', '#a6d5fa'],
      green: ['#28a745', '#1e7e34', '#155724', '#d4edda', '#c3e6cb'],
      warm: ['#ffc107', '#fd7e14', '#dc3545', '#e83e8c', '#6f42c1'],
      cool: ['#007bff', '#20c997', '#6f42c1', '#e83e8c', '#dc3545']
    };
  }

  // Chart Configuration (for when chart.js is installed)
  getLineChartOptions(title: string, yAxisLabel?: string): any {
    return {
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        title: {
          display: true,
          text: title,
          font: {
            size: 16,
            weight: 'bold'
          }
        },
        legend: {
          display: true,
          position: 'top'
        }
      },
      scales: {
        x: {
          display: true,
          title: {
            display: true,
            text: 'Time Period'
          }
        },
        y: {
          display: true,
          title: {
            display: true,
            text: yAxisLabel || 'Value'
          },
          beginAtZero: true
        }
      },
      elements: {
        line: {
          tension: 0.4
        },
        point: {
          radius: 4,
          hoverRadius: 6
        }
      }
    };
  }

  // Bar Chart Configuration
  getBarChartOptions(title: string, yAxisLabel?: string): any {
    return {
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        title: {
          display: true,
          text: title,
          font: {
            size: 16,
            weight: 'bold'
          }
        },
        legend: {
          display: true,
          position: 'top'
        }
      },
      scales: {
        x: {
          display: true,
          title: {
            display: true,
            text: 'Category'
          }
        },
        y: {
          display: true,
          title: {
            display: true,
            text: yAxisLabel || 'Count'
          },
          beginAtZero: true
        }
      }
    };
  }

  // Pie/Doughnut Chart Configuration
  getPieChartOptions(title: string): any {
    return {
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        title: {
          display: true,
          text: title,
          font: {
            size: 16,
            weight: 'bold'
          }
        },
        legend: {
          display: true,
          position: 'right'
        }
      }
    };
  }

  // Prepare Trend Data for Charts
  prepareTrendData(rawData: { month: string; value: number }[], label: string, color: string): any {
    return {
      labels: rawData.map(item => this.formatMonthLabel(item.month)),
      datasets: [{
        label: label,
        data: rawData.map(item => item.value),
        borderColor: color,
        backgroundColor: color + '20', // Add transparency
        fill: true,
        tension: 0.4
      }]
    };
  }

  // Prepare Breakdown Data for Charts
  prepareBreakdownData(rawData: { category: string; count: number; percentage: number }[], label: string): any {
    const colors = this.getColorSchemes().primary;

    return {
      labels: rawData.map(item => `${item.category} (${item.percentage.toFixed(1)}%)`),
      datasets: [{
        label: label,
        data: rawData.map(item => item.count),
        backgroundColor: colors.slice(0, rawData.length),
        borderWidth: 2,
        borderColor: '#ffffff'
      }]
    };
  }

  // Prepare Multi-Dataset Comparison
  prepareComparisonData(datasets: { label: string; data: number[]; color: string }[]): any {
    return {
      labels: [], // Would be set based on the data structure
      datasets: datasets.map(dataset => ({
        label: dataset.label,
        data: dataset.data,
        borderColor: dataset.color,
        backgroundColor: dataset.color + '40',
        fill: false,
        tension: 0.4
      }))
    };
  }

  // Format Month Labels for Better Readability
  private formatMonthLabel(monthStr: string): string {
    const [year, month] = monthStr.split('-');
    const date = new Date(parseInt(year), parseInt(month) - 1);
    return date.toLocaleDateString('en-US', { month: 'short', year: 'numeric' });
  }

  // Generate Gradient Colors
  generateGradient(color: string, opacity: number = 0.2): string {
    // This would generate actual gradients with Chart.js plugins
    return color + Math.round(opacity * 255).toString(16).padStart(2, '0');
  }

  // Chart Animation Options
  getAnimationOptions(duration: number = 1000) {
    return {
      duration: duration,
      easing: 'easeInOutQuart' as const
    };
  }

  // Responsive Chart Options
  getResponsiveOptions(): any {
    return {
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        legend: {
          display: true,
          position: 'top',
          labels: {
            usePointStyle: true,
            padding: 20
          }
        }
      }
    };
  }

  // Export Chart as Image (would need additional libraries)
  exportChartAsImage(chartId: string, filename: string = 'chart'): void {
    // This would use libraries like html2canvas or chart.js export plugins
    console.log(`Exporting chart ${chartId} as ${filename}`);
    // Implementation would depend on installed chart export libraries
  }

  // Chart Accessibility Options
  getAccessibleOptions(title: string): any {
    return {
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        title: {
          display: true,
          text: title,
          font: {
            size: 16,
            weight: 'bold'
          }
        },
        legend: {
          display: true,
          position: 'top',
          labels: {
            generateLabels: (chart: any) => {
              return chart.data.labels?.map((label: any, index: any) => ({
                text: `${label}: ${chart.data.datasets[0].data[index]}`,
                fillStyle: chart.data.datasets[0].backgroundColor,
                index: index
              })) || [];
            }
          }
        }
      }
    };
  }
}