-- Create a procedure stub and grant exec if it doesn't exist yet
if not exists (select 1 from sysobjects where id = object_id(N'InsertAppointment'))
begin
  exec('create procedure InsertAppointment as set nocount on;')
  grant exec on InsertAppointment to public
end
go

set quoted_identifier off
set ansi_nulls on
go

alter proc InsertAppointment
           @DoctorId   int,
           @PatientId  int,
           @DateStart  datetime,
           @DateEnd  datetime
as
begin

  set transaction isolation level read uncommitted
  
  declare @AppointmentId int
  
  insert into Appointment (DoctorId, PatientId, DateStart, DateEnd, Attended)
  select @DoctorId, @PatientId, @DateStart, @DateEnd, 0
  
  set @AppointmentId = scope_identity()

  select top 1 *
    from Appointment
   where AppointmentId = @AppointmentId

  return 0

end
go

/* -----------------------------------------
test script
----------------------------------------- */