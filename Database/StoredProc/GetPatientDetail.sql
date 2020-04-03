-- Create a procedure stub and grant exec if it doesn't exist yet
if not exists (select 1 from sysobjects where id = object_id(N'GetPatientDetail'))
begin
  exec('create procedure GetPatientDetail as set nocount on;')
  grant exec on GetPatientDetail to public
end
go

set quoted_identifier off
set ansi_nulls on
go

alter proc GetPatientDetail
           @UserId   int
as
begin

  select *
    from PatientDetail
   where [Userid] = @UserId

  return 0

end
go

/* -----------------------------------------
test script
----------------------------------------- */