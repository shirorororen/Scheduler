-- Create a procedure stub and grant exec if it doesn't exist yet
if not exists (select 1 from sysobjects where id = object_id(N'InsertPatientDetail'))
begin
  exec('create procedure InsertPatientDetail as set nocount on;')
  grant exec on InsertPatientDetail to public
end
go

set quoted_identifier off
set ansi_nulls on
go

alter proc InsertPatientDetail
           @Address varchar(20),
           @PhoneNo   varchar(20),
           @Email   varchar(20),
           @Age     varchar(20),
           @Gender  varchar(1),
           @UserId  bit

as
begin

  set transaction isolation level read uncommitted
  
  declare @DetailNo int
  
  insert into PatientDetail ([Address], PhoneNo, Email, Age, Gender, UserId)
  select @Address, @PhoneNo, @Email, @Age, @Gender, @UserId
  
  set @DetailNo = scope_identity()

  select * 
    from PatientDetail
   where DetailNo = @DetailNo

  return 0

end
go

/* -----------------------------------------
test script
----------------------------------------- */