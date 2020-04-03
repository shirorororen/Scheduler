import { Base } from './base';

export interface Appointment extends Base {
    AppointmentId: number;
    DoctorName: string;
    PatientName: string;
    DateStart: Date | string;
    DateEnd: Date | string;
    Attended: boolean;
}