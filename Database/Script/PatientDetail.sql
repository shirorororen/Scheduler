create table PatientDetail (
  DetailNo int primary key identity(1,1),
  [Address] varchar(100),
  [PhoneNo] varchar(20),
  [Email] varchar(30),
  [Age] int,
  [Gender] varchar(1),
  [UserID] int not null
)

