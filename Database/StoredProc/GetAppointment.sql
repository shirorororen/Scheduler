-- Create a procedure stub and grant exec if it doesn't exist yet
if not exists (select 1 from sysobjects where id = object_id(N'GetAppointment'))
begin
  exec('create procedure GetAppointment as set nocount on;')
  grant exec on GetAppointment to public
end
go

set quoted_identifier off
set ansi_nulls on
go

alter proc GetAppointment
           @AppointmentId   int = null
as
begin

  select *
    from Appointment
   where AppointmentId = @AppointmentId

  return 0

end
go

/* -----------------------------------------
test script
----------------------------------------- */