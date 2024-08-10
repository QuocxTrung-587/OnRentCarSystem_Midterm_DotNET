--use master;
--use master; create database OnRentCarSystemDB;
--use master; drop database OnRentCarSystemDB;
go
use OnRentCarSystemDB;
go
create table Roles (
	RoleID int,
	RoleName nvarchar(10),
	primary key(RoleID)
);

insert into Roles values 
(0, 'admin'),
(1, 'user');

create table Users(
	PhoneNumber varchar(10),
	--Username varchar(50),
	Pass varchar(50),
	UserFullName nvarchar(50),
	UserDateOfBirth date,
	UserGender int,
	UserEmail varchar(50),
	UserAddress nvarchar(50),
	UserRole int,
	primary key(PhoneNumber),
	foreign key(UserRole) references Roles(RoleID)
);

insert into Users values ('admin', '123456', null, null, 0, null, null, 0);

create table DriverLisense(
	LisenseID varchar(50),
	DriverFullName nvarchar(50),
	DriverDateOfBirth date,
	DriverImage image,
	PhoneNumber varchar(10),
	primary key(LisenseID),
	foreign key(PhoneNumber) references Users(PhoneNumber)
);

create table CarType(
	CarTypeID int,
	CarTypeName nvarchar(50),
	primary key(CarTypeID)
);

insert into CarType values 
(0, '4 seats (Mini)'),
(1, '4 seats (Sedan)'),
(2, '4 seats (Hatchback)'),
(3, '5 seats (CUV high chassis)'),
(4, '7 seats (SUV high chassis)'),
(5, '7 seats (MPV low chassis)'),
(6, 'Pickup truck');

create table Fuel(
	FuelID int,
	FuelName nvarchar(20)
	primary key(FuelID)
);

insert into Fuel values 
(0, 'Gasoline'),
(1, 'Diesel'),
(2, 'Electricity');

create table Cars(
	CarID varchar(50),
	CarTypeID int,
	FuelID int,
	IsRent int,
	CarPrice int,
	primary key(CarID),
	foreign key(CarTypeID) references CarType(CarTypeID),
	foreign key(FuelID) references Fuel(FuelID)
);

create table CarFeature(
	CarID varchar(50),
	CarMap int,
	CarBluetooth int,
	CarBackwardCam int,
	CarCurbsideCam int,
	CarJourneyCam int,
	CarSpeedWarning int,
	CarTireSensor int,
	CarCollisionSensor int,
	CarSunroof int,
	CarGPS int,
	CarUSB int,
	CarSubTire int,
	CarTrunkLid int,
	CarCam360 int,
	primary key(CarID),
	foreign key(CarID) references Cars(CarID)
);

create table Bill(
	BillID int identity(1, 1),
	UserPhoneNumber varchar(10),
	CarID varchar(50),
	DateCreate datetime,
	IsRent int,
	DateRent datetime,
	DateReturn datetime,
	TimeRent time,
	Total float,
	primary key(BillID),
	foreign key(UserPhoneNumber) references Users(PhoneNumber),
	foreign key(CarID) references Cars(CarID)
);
