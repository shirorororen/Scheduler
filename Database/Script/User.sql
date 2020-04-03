create table [User](
  UserId int identity(1,1) primary key,
  FirstName varchar(20) not null,
  LastName varchar(20) not null,
  Username varchar(20) not null,
  [Password] nvarchar(20) not null,
  Token nvarchar(30),
  UserTypeNo int,
  [Admin] bit
)


