create table Appointment (
 AppointmentId int primary key identity(1, 1),
 DoctorId int not null,
 PatientId int not null,
 DateStart datetime not null,
 DateEnd datetime not null,
 Attended bit not null
)
