IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'msc_str') 
BEGIN
    EXEC('CREATE SCHEMA Farm');
END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE SCHEMA_NAME(schema_id) like 'Farm' and name like 'class') CREATE TABLE Farm.class (
    IDClass uniqueidentifier not null,
	Class varchar(1) not null,
	ClassDescription varchar(200) not null,
	constraint PK_Class primary key (IDClass)
);

IF NOT EXISTS (SELECT * FROM sys.tables WHERE SCHEMA_NAME(schema_id) like 'Farm' and name like 'product_type') CREATE TABLE Farm.product_type (
    IDProductType uniqueidentifier not null,
	Category varchar(30) not null,
	CategoryDescription varchar(200) not null,
	constraint PK_Product_Type primary key (IDProductType)
);

IF NOT EXISTS (SELECT * FROM sys.tables WHERE SCHEMA_NAME(schema_id) like 'Farm' and name like 'users') CREATE TABLE Farm.users (
    IDUser uniqueidentifier not null,
	ProfilePictureURL varchar(100),
	Email varchar(40) not null,
	UserPassword varchar(25) not null,
	FirstName varchar(30) not null,
	LastName varchar(30) not null,
	Adress varchar(50) not null,
	City varchar(30) not null,
	Phone varchar(12) not null,
	UserRole varchar(6) not null,
	constraint PK_Users primary key (IDUser)
);

IF NOT EXISTS (SELECT * FROM sys.tables WHERE SCHEMA_NAME(schema_id) like 'Farm' and name like 'origin') CREATE TABLE Farm.origin (
    IDOrigin uniqueidentifier  not null,
	OriginName varchar(15) not null,
	OriginDescription varchar(200) not null,
	constraint PK_Origin primary key (IDOrigin)
);

IF NOT EXISTS (SELECT * FROM sys.tables WHERE SCHEMA_NAME(schema_id) like 'Farm' and name like 'product') CREATE TABLE Farm.product (
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
);

IF NOT EXISTS (SELECT * FROM sys.tables WHERE SCHEMA_NAME(schema_id) like 'Farm' and name like 'orders') CREATE TABLE Farm.orders (
    IDOrder uniqueidentifier not null,
	IDUser uniqueidentifier not null,
	TotalOrderPrice numeric(10) not null,
	TransactionDate date not null,
	constraint PK_Order primary key (IDOrder),
	constraint FK_Order_Users foreign key (IDUser)
		references Farm.users (IDUser)
		ON DELETE CASCADE
);

IF NOT EXISTS (SELECT * FROM sys.tables WHERE SCHEMA_NAME(schema_id) like 'Farm' and name like 'order_item') CREATE TABLE Farm.order_item (
    IDOrderItem uniqueidentifier not null,
	IDOrder uniqueidentifier not null,
	IDProduct uniqueidentifier not null,
	OrderAmount numeric(5) not null,
	OrderPrice numeric(8) not null,
	constraint PK_Order_Item primary key (IDOrderItem),
	constraint FK_Order_Item_Order foreign key (IDOrder)
		references Farm.orders (IDOrder)
		ON DELETE CASCADE,
	constraint FK_Order_Item_Product foreign key (IDProduct)
		references Farm.product (IDProduct)
);

IF NOT EXISTS (SELECT * FROM sys.tables WHERE SCHEMA_NAME(schema_id) like 'Farm' and name like 'cart_item') CREATE TABLE Farm.cart_item (
    IDCartItem uniqueidentifier not null,
	IDUser uniqueidentifier not null,
	IDProduct uniqueidentifier not null,
	CartAmount numeric(5) not null,
	CartPrice numeric(8) not null,
	constraint PK_Cart_Item primary key (IDCartItem),
	constraint FK_Cart_Item_User foreign key (IDUser)
		references Farm.users (IDUser)
		ON DELETE CASCADE,
	constraint FK_Cart_Item_Product foreign key (IDProduct)
		references Farm.product (IDProduct)
);