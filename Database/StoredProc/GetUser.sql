-- Create a procedure stub and grant exec if it doesn't exist yet
if not exists (select 1 from sysobjects where id = object_id(N'GetUser'))
begin
  exec('create procedure GetUser as set nocount on;')
  grant exec on GetUser to public
end
go

set quoted_identifier off
set ansi_nulls on
go

alter proc GetUser
           @UserId   int = null,
           @Username varchar(20) = null,
           @Password nvarchar(20) = null
as
begin

  select *
    from [User]
   where [Userid] = @UserId
     or (Username = @Username and [Password] = @Password)

  return 0
end
go

/* -----------------------------------------
test script
----------------------------------------- */