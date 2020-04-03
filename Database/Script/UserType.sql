create table UserType(
  UserTypeNo int identity(1,1) primary key,
  [Description] varchar(50) not null
)


insert into UserType
values ('Staff'), ('Patient'), ('Doctor')
