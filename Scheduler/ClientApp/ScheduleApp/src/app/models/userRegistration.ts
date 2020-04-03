import { UserAccount } from './userAccount';
import { PatientDetails } from './patientDetail';

export interface UserRegistration {
    userAccount : UserAccount,
    patientDetail : PatientDetails
}