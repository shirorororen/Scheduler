-- Create a procedure stub and grant exec if it doesn't exist yet
if not exists (select 1 from sysobjects where id = object_id(N'UpdateAppointment'))
begin
  exec('create procedure UpdateAppointment as set nocount on;')
  grant exec on UpdateAppointment to public
end
go

set quoted_identifier off
set ansi_nulls on
go

alter proc UpdateAppointment
           @AppointmentId   int,
           @Attended bit
as
begin

  set transaction isolation level read uncommitted
  
  update Appointment
     set Attended = @Attended
   where AppointmentId = @AppointmentId


  select top 1 *
    from Appointment
   where AppointmentId = @AppointmentId

  return 0

end
go

/* -----------------------------------------
test script
----------------------------------------- */