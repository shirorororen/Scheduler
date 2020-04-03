-- Create a procedure stub and grant exec if it doesn't exist yet
if not exists (select 1 from sysobjects where id = object_id(N'InsertUser'))
begin
  exec('create procedure InsertUser as set nocount on;')
  grant exec on InsertUser to public
end
go

set quoted_identifier off
set ansi_nulls on
go

alter proc InsertUser
           @FirstName  varchar(20),
           @LastName   varchar(20),
           @Username   varchar(20),
           @Password   varchar(20),
           @UserTypeNo int,
           @Admin      bit

as
begin

  set transaction isolation level read uncommitted
  
  declare @UserId int
  
  insert into [User] (FirstName, LastName, Username, [Password], UserTypeNo, [Admin])
  select @FirstName, @LastName, @Username, @Password, @UserTypeNo, @Admin
  
  set @UserId = scope_identity()

  select top 1 *
    from [User]
   where UserId = @UserId

  return 0

end
go

/* -----------------------------------------
test script
----------------------------------------- */