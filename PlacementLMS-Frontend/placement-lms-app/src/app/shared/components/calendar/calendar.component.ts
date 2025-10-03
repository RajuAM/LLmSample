import { Component, Input, Output, EventEmitter, OnInit, OnChanges, SimpleChanges } from '@angular/core';

export interface CalendarEvent {
  id: string;
  title: string;
  date: Date;
  type: 'assignment' | 'test' | 'interview' | 'deadline' | 'course' | 'other';
  description?: string;
  startTime?: string;
  endTime?: string;
  location?: string;
}

export interface CalendarDay {
  date: Date;
  isCurrentMonth: boolean;
  isToday: boolean;
  events: CalendarEvent[];
}

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit, OnChanges {
  @Input() events: CalendarEvent[] = [];
  @Input() view: 'month' | 'week' | 'day' = 'month';
  @Input() selectedDate: Date = new Date();
  @Output() dateSelected = new EventEmitter<Date>();
  @Output() eventSelected = new EventEmitter<CalendarEvent>();

  currentDate: Date = new Date();
  calendarDays: CalendarDay[] = [];
  weekDays: string[] = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
  monthNames: string[] = [
    'January', 'February', 'March', 'April', 'May', 'June',
    'July', 'August', 'September', 'October', 'November', 'December'
  ];

  // Week view properties
  weekDaysData: CalendarDay[] = [];

  ngOnInit(): void {
    this.generateCalendar();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['events'] || changes['selectedDate']) {
      this.generateCalendar();
    }
  }

  // Generate calendar based on current view
  generateCalendar(): void {
    if (this.view === 'month') {
      this.generateMonthView();
    } else if (this.view === 'week') {
      this.generateWeekView();
    } else {
      this.generateDayView();
    }
  }

  // Generate month view
  generateMonthView(): void {
    const year = this.selectedDate.getFullYear();
    const month = this.selectedDate.getMonth();

    const firstDay = new Date(year, month, 1);
    const lastDay = new Date(year, month + 1, 0);
    const startDate = new Date(firstDay);
    startDate.setDate(startDate.getDate() - firstDay.getDay());

    const endDate = new Date(lastDay);
    endDate.setDate(endDate.getDate() + (6 - lastDay.getDay()));

    this.calendarDays = [];

    let currentDate = new Date(startDate);
    while (currentDate <= endDate) {
      const dayEvents = this.getEventsForDate(currentDate);
      const calendarDay: CalendarDay = {
        date: new Date(currentDate),
        isCurrentMonth: currentDate.getMonth() === month,
        isToday: this.isToday(currentDate),
        events: dayEvents
      };

      this.calendarDays.push(calendarDay);
      currentDate.setDate(currentDate.getDate() + 1);
    }
  }

  // Generate week view
  generateWeekView(): void {
    const startOfWeek = new Date(this.selectedDate);
    startOfWeek.setDate(startOfWeek.getDate() - startOfWeek.getDay());

    this.weekDaysData = [];

    for (let i = 0; i < 7; i++) {
      const date = new Date(startOfWeek);
      date.setDate(startOfWeek.getDate() + i);

      const dayEvents = this.getEventsForDate(date);
      const calendarDay: CalendarDay = {
        date: new Date(date),
        isCurrentMonth: true,
        isToday: this.isToday(date),
        events: dayEvents
      };

      this.weekDaysData.push(calendarDay);
    }
  }

  // Generate day view
  generateDayView(): void {
    const dayEvents = this.getEventsForDate(this.selectedDate);
    const calendarDay: CalendarDay = {
      date: new Date(this.selectedDate),
      isCurrentMonth: true,
      isToday: this.isToday(this.selectedDate),
      events: dayEvents
    };

    this.calendarDays = [calendarDay];
  }

  // Get events for a specific date
  getEventsForDate(date: Date): CalendarEvent[] {
    return this.events.filter(event => {
      const eventDate = new Date(event.date);
      return eventDate.toDateString() === date.toDateString();
    });
  }

  // Check if date is today
  isToday(date: Date): boolean {
    const today = new Date();
    return date.toDateString() === today.toDateString();
  }

  // Navigate to previous period
  previousPeriod(): void {
    if (this.view === 'month') {
      this.selectedDate = new Date(this.selectedDate.getFullYear(), this.selectedDate.getMonth() - 1, 1);
    } else if (this.view === 'week') {
      this.selectedDate = new Date(this.selectedDate.getTime() - 7 * 24 * 60 * 60 * 1000);
    } else {
      this.selectedDate = new Date(this.selectedDate.getTime() - 24 * 60 * 60 * 1000);
    }
    this.generateCalendar();
    this.dateSelected.emit(this.selectedDate);
  }

  // Navigate to next period
  nextPeriod(): void {
    if (this.view === 'month') {
      this.selectedDate = new Date(this.selectedDate.getFullYear(), this.selectedDate.getMonth() + 1, 1);
    } else if (this.view === 'week') {
      this.selectedDate = new Date(this.selectedDate.getTime() + 7 * 24 * 60 * 60 * 1000);
    } else {
      this.selectedDate = new Date(this.selectedDate.getTime() + 24 * 60 * 60 * 1000);
    }
    this.generateCalendar();
    this.dateSelected.emit(this.selectedDate);
  }

  // Go to today
  goToToday(): void {
    this.selectedDate = new Date();
    this.generateCalendar();
    this.dateSelected.emit(this.selectedDate);
  }

  // Select date
  selectDate(date: Date): void {
    this.selectedDate = date;
    this.dateSelected.emit(date);
  }

  // Select event
  selectEvent(event: CalendarEvent): void {
    this.eventSelected.emit(event);
  }

  // Get event type icon
  getEventIcon(type: string): string {
    switch (type) {
      case 'assignment': return 'fas fa-tasks';
      case 'test': return 'fas fa-clipboard-list';
      case 'interview': return 'fas fa-users';
      case 'deadline': return 'fas fa-exclamation-triangle';
      case 'course': return 'fas fa-book';
      default: return 'fas fa-calendar-alt';
    }
  }

  // Get event type color
  getEventColor(type: string): string {
    switch (type) {
      case 'assignment': return '#ffc107';
      case 'test': return '#17a2b8';
      case 'interview': return '#28a745';
      case 'deadline': return '#dc3545';
      case 'course': return '#007bff';
      default: return '#6c757d';
    }
  }

  // Format time display
  formatTime(date: Date): string {
    return date.toLocaleTimeString('en-US', {
      hour: 'numeric',
      minute: '2-digit',
      hour12: true
    });
  }

  // Get current period display text
  getPeriodDisplay(): string {
    if (this.view === 'month') {
      return this.monthNames[this.selectedDate.getMonth()] + ' ' + this.selectedDate.getFullYear();
    } else if (this.view === 'week') {
      const startOfWeek = new Date(this.selectedDate);
      startOfWeek.setDate(this.selectedDate.getDate() - this.selectedDate.getDay());
      const endOfWeek = new Date(startOfWeek);
      endOfWeek.setDate(startOfWeek.getDate() + 6);

      if (startOfWeek.getMonth() === endOfWeek.getMonth()) {
        return `${this.monthNames[startOfWeek.getMonth()]} ${startOfWeek.getDate()} - ${endOfWeek.getDate()}, ${startOfWeek.getFullYear()}`;
      } else {
        return `${this.monthNames[startOfWeek.getMonth()]} ${startOfWeek.getDate()} - ${this.monthNames[endOfWeek.getMonth()]} ${endOfWeek.getDate()}, ${startOfWeek.getFullYear()}`;
      }
    } else {
      return this.selectedDate.toLocaleDateString('en-US', {
        weekday: 'long',
        year: 'numeric',
        month: 'long',
        day: 'numeric'
      });
    }
  }

  // Get weeks for month view
  getWeeks(): CalendarDay[][] {
    const weeks: CalendarDay[][] = [];
    for (let i = 0; i < this.calendarDays.length; i += 7) {
      weeks.push(this.calendarDays.slice(i, i + 7));
    }
    return weeks;
  }

  // Check if date is selected
  isSelectedDate(date: Date): boolean {
    return date.toDateString() === this.selectedDate.toDateString();
  }
}