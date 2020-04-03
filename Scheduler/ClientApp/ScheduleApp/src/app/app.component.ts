import { Component, ViewChild, AfterViewInit } from '@angular/core';
import { jqxSchedulerComponent } from 'jqwidgets-ng/jqxscheduler';
import { Appointment } from './models/appointment';
import { SchedulerServiceService } from './services/scheduler-service.service';
import { DateBody } from './models/datebody';
@Component({
    selector: 'app-root',
    templateUrl: './app.component.html'
})
export class AppComponent implements AfterViewInit {
    doctorId: number;
    patientId: number;
    dateFilter: DateBody;
    endDate: Date;
    appointments : Appointment[];
    year: number;
    month: number;
    day: number;

    constructor(private service: SchedulerServiceService) {}
    ngOnInit() {
      // no log in yet
      this.endDate = new Date();
      this.endDate.setDate(this.endDate.getDate() + 7);
      this.doctorId = 1;
      this.patientId = 2;
      this.dateFilter = new DateBody();
      this.dateFilter.DateStart = new Date();
      this.dateFilter.DateEnd = this.endDate;

      let dateObj = new Date();
      this.month = dateObj.getUTCMonth() + 1; //months from 1-12
      this.day = dateObj.getUTCDate();
      this.year = dateObj.getUTCFullYear();
    }

    ngAfterContentInit() {
      this.generateAppointments();
      this.scheduler.source = this.source;
    }

    @ViewChild('schedulerReference', { static: false }) scheduler: jqxSchedulerComponent;

    ngAfterViewInit(): void {
        this.scheduler.ensureAppointmentVisible('id1');
    }

	  getWidth() : any {
      if (document.body.offsetWidth < 850) {
        return '90%';
      }
      return 850;
	  }

    generateAppointments() : any {
      this.service.getAppointments(this.doctorId, this.patientId, this.dateFilter)
        .subscribe(result => {
            if (result && result.length > 0) {
              this.appointments = result;
              this.source.localData = result;
              this.scheduler.source = this.source;
            }
        });
    }

    source: any =
    {
        dataType: "array",
        dataFields: [
            { name: 'AppointmentId', type: 'string' },
            { name: 'DoctorName', type: 'string' },
            { name: 'PatientName', type: 'string' },
            { name: 'DateStart', type: 'date' },
            { name: 'DateEnd', type: 'date' },
            { name: 'Attended', type: 'boolean' }
        ],
        id: 'AppointmentId',
        localData: this.appointments
    };
    dataAdapter: any = new jqx.dataAdapter(this.source);
    date: any = new jqx.date(this.year, this.month, this.day);
    appointmentDataFields: any =
    {
        from: "DateStart",
        to: "DateEnd",
        id: "AppointmentId",
        description: "PatientName",
        location: "DoctorName",
        subject: "subject",
        resourceId: "PatientName"
    };
    resources: any =
    {
        colorScheme: "scheme05",
        dataField: "PatientName",
        source: new jqx.dataAdapter(this.source)
    };
    views: any[] =
    [
        'dayView',
        'weekView',
        'monthView'
    ];  
}