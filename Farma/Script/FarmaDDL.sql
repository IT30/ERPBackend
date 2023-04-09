--Trigeri
if object_id('Farm.trigger1', 'tr') is not null
	drop trigger Farm.trigger1
go

if object_id('Farm.trigger2', 'tr') is not null
	drop trigger Farm.trigger2
go

--Tabele
if object_id('Farm.order_item', 'U') is not null
	drop table Farm.order_item
go

if object_id('Farm.cart_item', 'U') is not null
	drop table Farm.cart_item
go

if object_id('Farm.orders', 'U') is not null
	drop table Farm.orders
go

if object_id('Farm.product', 'U') is not null
	drop table Farm.product
go

if object_id('Farm.origin', 'U') is not null
	drop table Farm.origin
go

if object_id('Farm.users', 'U') is not null
	drop table Farm.users
go


if object_id('Farm.product_type', 'U') is not null
	drop table Farm.product_type
go

if object_id('Farm.class', 'U') is not null
	drop table Farm.class
go

if object_id('Farm.picture', 'U') is not null
	drop table Farm.picture
go

if SCHEMA_ID('Farm') is not null
	drop schema Farm
go

create schema Farm
go

create table Farm.class(
	IDClass uniqueidentifier not null,
	Class varchar(1) not null,
	ClassDescription varchar(200) not null,
	constraint PK_Class primary key (IDClass),
)

create table Farm.product_type(
	IDProductType uniqueidentifier not null,
	Category varchar(30) not null,
	CategoryDescription varchar(200) not null,
	constraint PK_Product_Type primary key (IDProductType),
)

create table Farm.users(
	IDUser uniqueidentifier not null,
	ProfilePictureURL varchar(100),
	Email varchar(40) not null,
	Username varchar(15) not null,
	FirstName varchar(30) not null,
	LastName varchar(30) not null,
	Adress varchar(50) not null,
	City varchar(30) not null,
	Phone varchar(12) not null,
	UserRole varchar(6) not null,
	PwdHash varchar(350) not null,
	PwdSalt varchar(15) not null,
	constraint PK_Users primary key (IDUser)
)

create table Farm.origin(
	IDOrigin uniqueidentifier  not null,
	OriginName varchar(15) not null,
	OriginDescription varchar(200) not null,
	constraint PK_Origin primary key (IDOrigin)
)

create table Farm.product(
	IDProduct uniqueidentifier not null,
	IDProductType uniqueidentifier not null,
	IDClass uniqueidentifier not null,
	IDOrigin uniqueidentifier not null,
	ProductName varchar(25) not null,
	SupplyKG numeric(6) not null,
	PriceKG numeric(6) not null,
	ProductPictureURL varchar(100) not null,
	ProductDescription varchar(400) not null,
	DiscountPercentage numeric(3) not null,
	constraint PK_Product primary key (IDProduct),
	constraint FK_Product_Product_Type foreign key (IDProductType)
		references Farm.product_type (IDProductType),
	constraint FK_Product_Class foreign key (IDClass)
		references Farm.class (IDClass),
	constraint FK_Product_Origin foreign key (IDOrigin)
		references Farm.origin (IDOrigin)
)

create table Farm.orders(
	IDOrder uniqueidentifier not null,
	IDUser uniqueidentifier not null,
	TotalOrderPrice numeric(10) not null,
	TransactionDate date not null,
	constraint PK_Order primary key (IDOrder),
	constraint FK_Order_Users foreign key (IDUser)
		references Farm.users (IDUser)
)

create table Farm.order_item(
	IDOrderItem uniqueidentifier not null,
	IDOrder uniqueidentifier not null,
	IDProduct uniqueidentifier not null,
	OrderAmount numeric(5) not null,
	OrderPrice numeric(8) not null,
	constraint PK_Order_Item primary key (IDOrderItem),
	constraint FK_Order_Item_Order foreign key (IDOrder)
		references Farm.orders (IDOrder),
	constraint FK_Order_Item_Product foreign key (IDProduct)
		references Farm.product (IDProduct)
)

create table Farm.cart_item(
	IDCartItem uniqueidentifier not null,
	IDUser uniqueidentifier not null,
	IDProduct uniqueidentifier not null,
	CartAmount numeric(5) not null,
	CartPrice numeric(8) not null,
	constraint PK_Cart_Item primary key (IDCartItem),
	constraint FK_Cart_Item_User foreign key (IDUser)
		references Farm.users (IDUser),
	constraint FK_Cart_Item_Product foreign key (IDProduct)
		references Farm.product (IDProduct)
)
go
--Trigeri
create trigger Farm.trigger1
on Farm.order_item
after insert
as
begin
	declare @idpr uniqueidentifier = (select IDProduct from inserted),
			@kolicinapr numeric(5),
			@kupljeno numeric(5) = (select OrderAmount from inserted)
	set @kolicinapr = (select SupplyKG from Farm.product where IDProduct=@idpr)

	if(@kupljeno>0 and @kolicinapr>=@kupljeno)
	begin
		update Farm.product
		set SupplyKG = @kolicinapr - @kupljeno
		where IDProduct=@idpr
	end
	else
		RAISERROR('Nema dovoljno kolicine za ovu kupovinu', 16, 0);
end
go

/*
create trigger Farm.trigger2
on Farm.storage
after insert, update
as
begin
	declare @idsk numeric(5) = (select IDSkladiste from inserted),
			@datumpak date,
			@datumrok date
	set @datumpak = (select DatumSkladistenja from Farm.storage where IDSkladiste=@idsk)
	set @datumrok = (select RokTrajanja from Farm.storage where IDSkladiste=@idsk)

	if(@datumrok<@datumpak)
	begin
		RAISERROR('Nije moguce uneti rok trajanja raniji od datuma pakovanja', 16, 0);

		delete Farm.storage
		where IDSkladiste=@idsk
	end
end
go
*/