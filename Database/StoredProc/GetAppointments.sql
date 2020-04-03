-- Create a procedure stub and grant exec if it doesn't exist yet
if not exists (select 1 from sysobjects where id = object_id(N'GetAppointments'))
begin
  exec('create procedure GetAppointments as set nocount on;')
  grant exec on GetAppointments to public
end
go

set quoted_identifier off
set ansi_nulls on
go

alter proc GetAppointments
           @DoctorId   int = null,
           @PatientId  int = null,
           @DateStart  datetime = null,
           @DateEnd    datetime = null
as
begin

  select *
    from Appointment
   where DoctorId = @DoctorId
      or PatientId = @PatientId
     and DateStart >= ISNULL(@DateStart, DateStart)
     and DateEnd <= ISNULL(@DateEnd, DateEnd)

  return 0

end
go

/* -----------------------------------------
test script
----------------------------------------- */