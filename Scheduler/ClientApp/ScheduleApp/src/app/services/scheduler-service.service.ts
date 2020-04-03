import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { UserDetail } from '../models/userDetail';
import { UserRegistration } from '../models/userRegistration';
import { DateBody } from '../models/datebody';
import { Appointment } from '../models/appointment';

@Injectable({
  providedIn: 'root'
})
export class SchedulerServiceService {

  constructor(private httpClient : HttpClient) { }

  login (username: string, password: string) {
    return this.httpClient.get<UserDetail>(
      `${environment.url}/api/scheduler/login/${username}/${password}`
    );
  }

  register (userRegistration: UserRegistration) {
    return this.httpClient.post<UserDetail>(
      `${environment.url}/api/scheduler/register`, userRegistration
    );
  }

  getAppointments (doctorId: number, patientId: number, dateFilter : DateBody) {
    return this.httpClient.post<Appointment[]>(
      `${environment.url}/api/scheduler/appointments/${doctorId}/${patientId}`, dateFilter
    );
  }

  createAppointment (doctorId: number, patientId: number, dateFilter : DateBody) {
    return this.httpClient.post<Appointment>(
      `${environment.url}/api/scheduler/appoint/${doctorId}/${patientId}`, dateFilter
    );
  }

  updateAppointment (id: number, attended: boolean) {
    return this.httpClient.put<Appointment>(
      `${environment.url}/api/scheduler/appoint/${id}/${attended}/update`, {}
    );
  }
}
