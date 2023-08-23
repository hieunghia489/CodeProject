create database FUFlowerBouquetManagement;
use FUFlowerBouquetManagement; 
create table Customer (
CustomerID nvarchar(50) primary key,
Email nvarchar(50)  not null unique check ( Email like '%@%.%'),
CustomerName nvarchar(100) not null,
City nvarchar(50) not null,
Password nvarchar(50) not null,
Birthday date not null
);
Insert Customer (CustomerID,Email,CustomerName,City,Password, Birthday) values ('SE01','ToitenA@gmail.com','Nguyen Van A','Hai Phong', '123456', '10-3-2001');
Insert Customer (CustomerID,Email,CustomerName,City,Password, Birthday) values ('SE02','MyNameB@gmail.com','Le Thi B','Ha Noi', '123456', '5-15-2001');
Insert Customer (CustomerID,Email,CustomerName,City,Password, Birthday) values ('SE03','ToilaC@gmail.com','Tran Van C','Vung Tau', '123456', '8-20-2001');
go
create table Supplier (
SupplierID nvarchar(50) primary key,
SupplierName nvarchar(100) not null,
SupplierAddress text not null,
Telephone nvarchar(10) check ( Telephone not like '0%[^0-9]%') unique not null
);
insert Supplier (SupplierID,SupplierName,SupplierAddress,Telephone) values ('SP01','Supplier 1','Quan 9 Ho Chi Minh','0939393939');
insert Supplier (SupplierID,SupplierName,SupplierAddress,Telephone) values ('SP02','Supplier 2','Quan Go Vap Ho Chi Minh','0979797979');
go
create table Category (
CategoryID nvarchar(50) primary key,
CategoryName nvarchar(50) not null,
CategoryDescription text not null
);
insert Category(CategoryID,CategoryName,CategoryDescription) values ('Smell','Hoa Thom','Hoa co mui huong thom tho');
insert Category(CategoryID,CategoryName,CategoryDescription) values ('Color','Hoa Dep','Hoa co nhieu mau sac');

go
create table FlowerBouquet (
FlowerBouquetID nvarchar(20) primary key,
CategoryID nvarchar(50) foreign key references Category(CategoryID),
FlowerName nvarchar(50) not null,
Description text not null,
UnitPrice money not null,
UnitInStock int check (UnitInStock > 0 or UnitInStock = 0) not null,
FlowerStatus bit default 1 not null,
SupplierID nvarchar(50) foreign key references Supplier(SupplierID)
);
insert FlowerBouquet(FlowerBouquetID,CategoryID,FlowerName,Description,UnitPrice,UnitInStock,FlowerStatus,SupplierID) values ('S1','Smell','Hoa Nhai','Hoa thom lam','100000','5',1,'SP01');
insert FlowerBouquet(FlowerBouquetID,CategoryID,FlowerName,Description,UnitPrice,UnitInStock,FlowerStatus,SupplierID) values ('S2','Smell','Hoa Lai','Hoa rat thom','150000','5',1,'SP01');
insert FlowerBouquet(FlowerBouquetID,CategoryID,FlowerName,Description,UnitPrice,UnitInStock,FlowerStatus,SupplierID) values ('C1','Color','Hoa Hong','Hoa co mau do, den hoac trang','50000','10',1,'SP02');
insert FlowerBouquet(FlowerBouquetID,CategoryID,FlowerName,Description,UnitPrice,UnitInStock,FlowerStatus,SupplierID) values ('C2','Color','Hoa Tulip','Hoa dep lam','100000','5',1,'SP02');
insert FlowerBouquet(FlowerBouquetID,CategoryID,FlowerName,Description,UnitPrice,UnitInStock,FlowerStatus,SupplierID) values ('C3','Color','Hoa Lan','Hoa xinh lam','100000','5',1,'SP01');

go
create table "Order" (
OrderID nvarchar(50) primary key,
CustomerID nvarchar(50) foreign key references Customer(CustomerID),
OrderDate date not null,
ShipDate date not null,
Freight money not null,
Total money check (Total > 0 ) not null default 0,
OrderStatus bit default 1 not null
);
insert "Order"(OrderID,CustomerID,OrderDate,ShipDate,Freight,Total,OrderStatus) values ('001','SE01','1-1-2023','1-3-2023','25000','300000',1);
insert "Order"(OrderID,CustomerID,OrderDate,ShipDate,Freight,Total,OrderStatus) values ('002','SE02','2-2-2023','2-4-2023','25000','200000',1);
insert "Order"(OrderID,CustomerID,OrderDate,ShipDate,Freight,Total,OrderStatus) values ('003','SE03','4-4-2023','6-4-2023','25000','500000',1);
go

create table OrderDetail (
OrderID nvarchar(50) foreign key references "Order"(OrderID) not null,
FlowerBouquetID nvarchar(20) foreign key references FlowerBouquet(FlowerBouquetID) not null,
UnitPrice money not null,
Quantity int check (Quantity > 0) not null,
Discount float default 0 not null
);
insert OrderDetail (OrderID,FlowerBouquetID,UnitPrice,Quantity,Discount) values ('001','S1','100000','2','0');
insert OrderDetail (OrderID,FlowerBouquetID,UnitPrice,Quantity,Discount) values ('001','C1','50000','2','0');
insert OrderDetail (OrderID,FlowerBouquetID,UnitPrice,Quantity,Discount) values ('002','C2','100000','1','0');
insert OrderDetail (OrderID,FlowerBouquetID,UnitPrice,Quantity,Discount) values ('002','C3','100000','1','0');
insert OrderDetail (OrderID,FlowerBouquetID,UnitPrice,Quantity,Discount) values ('003','S1','100000','2','0');
insert OrderDetail (OrderID,FlowerBouquetID,UnitPrice,Quantity,Discount) values ('003','S2','150000','2','0');
alter table OrderDetail add CONSTRAINT orderdetail_pk PRIMARY KEY (OrderID, FlowerBouquetID); --tao 2 khoa chinh

