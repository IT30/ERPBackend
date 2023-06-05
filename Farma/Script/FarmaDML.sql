insert into Farm.class(IDClass, Class, ClassDescription)
values ('7ff7b7b4-7323-4eef-a29b-22361d873c80','A', 'Najbolji kvalitet')
insert into Farm.class(IDClass, Class, ClassDescription)
values ('d50a8b84-7754-4a7d-aa3f-68ad5a656d3c','B', 'Srednji kvalitet')
go

insert into Farm.product_type(IDProductType,Category,CategoryDescription)
values('7daefd85-1814-4d7c-9440-24d1db7d85eb','Meso','Domace meso od zivotinja sa nase farme')
insert into Farm.product_type(IDProductType,Category,CategoryDescription)
values('784680bd-c94a-4528-8f50-6241cdb93e9c','Životinjski proizvodi','Prirodni produkti zivotinja sa naše farme')
insert into Farm.product_type(IDProductType,Category,CategoryDescription)
values('87f0b9f4-79ff-497a-a8b7-cdd3e2acdc17','Domaci proizvodi','Specijaliteti našeg kraja')
go

insert into Farm.users(IDUser,ProfilePictureURL, Email, Username, FirstName, LastName, Adress, City, Phone, UserRole, PwdHash, PwdSalt)
values ('c471030b-6af8-499a-977d-c006b9efcc95','8208fe67a521fd3fd8af5902f7292ccc.jpg', 'email@email.com', 'admin', 'Aca','Peric','Bulevar Oslobodjenja 5','Novi Sad','0624256164','ADMIN','u2soTV8wBLPl2H1gynQC9n7ilfzN5NbEVlAsVvqjX0hR4vF9NRJNpQAskd4cn4196h2613paMARJMSEVyFu7J9iRmqY7jHwGvL2GrxzF2KTAyB5RS6j6OIchTBMNB1EHgowUMDfrlLI96jYDWhktFQYGylEhFg/XBWZHPqtnBC3DSNabM55HLmtyuld3Rrp1ZI7khah79ggQyp0G1b2utfiocvGhw9cBa7GORRlDIgopdEcyXWSW/DRWz1V93fV6qQwqW3C6T9YBW5NdAB49R9pTLQp4cuvtQQGtcQVy+h8q8mO8bGajBwKhFvPlsTf8/W+kS3POGZCyTS4FcS7JAQ==', 'DigOR1g=')
insert into Farm.users(IDUser,ProfilePictureURL, Email, Username, FirstName, LastName, Adress, City, Phone, UserRole, PwdHash, PwdSalt)
values ('c16a5de9-ffd8-4c06-be6c-832dac4e7a6a','default.jpg', 'a', 'user1', 'aaaa','aaaa','Negde','Neki grad','0674562','USER','up0XsizlDVzI12vHexUWnf5fcTmi4sG9fzkh5kuBWAsA1DIpajrqq1T2fEIQ96bWwTC5Qq2zL+8QG0QSWZG16J/WlV8dOvBz859xTt6emL96Dn4kaeVSo8KcSVe458O0z1qfuo6s9G9+Up+LOQQ0abvs3KgodPCmxgHSP3vti2/jMAhQ6HBWQsEvvqQgH9YnKq0gHeA9QJ8T0gFT2t0U2HIw0Tde/5aKLdKaIBcxgadML0QrbosIiwDYyH9u6rLTaMPXledQ5RdbFr+ty7oHaKzkQTob/O+2YuUrJdGBkKn+SVw/wW96puJJxZo9KXVQ0iQjsyJGRn/e30SKAAxX0g==', '2ZY2dWk=')
go

insert into Farm.origin(IDOrigin,OriginName, OriginDescription)
values('9d590483-53d2-4ef9-8a2d-01d63795daf0','Mangulica','Rasna svinja')
insert into Farm.origin(IDOrigin,OriginName, OriginDescription)
values('20115872-a12e-4f06-b939-3f2fb54dd9f5','Plastenici','Svezi proizvodi iz plastenika')
insert into Farm.origin(IDOrigin,OriginName, OriginDescription)
values('820d312d-405a-4b86-80ce-45d1515eafc2','Tovni pilići','Bogati mesom')
insert into Farm.origin(IDOrigin,OriginName, OriginDescription)
values('7b573fd1-5826-4e0d-85d3-6b9ccb945faa','Koke nosilje','Rapidna proizvodnja jaja')
insert into Farm.origin(IDOrigin,OriginName, OriginDescription)
values('18ad44a5-de14-4cee-a1e9-72c965aad067','Holštajn','Krava mlekara')
go

insert into Farm.product(IDProduct,IDProductType,IDClass,IDOrigin,ProductName,SupplyKG,PriceKG,ProductPictureURL,ProductDescription,DiscountPercentage)
values('2d987d72-d226-48e5-9b9c-1b9bc026d8d3','784680bd-c94a-4528-8f50-6241cdb93e9c','7ff7b7b4-7323-4eef-a29b-22361d873c80','7b573fd1-5826-4e0d-85d3-6b9ccb945faa','Jaja',5000,20,'jaja-2-n1-117282 Cropped.jpg','Organska jaja',0)
insert into Farm.product(IDProduct,IDProductType,IDClass,IDOrigin,ProductName,SupplyKG,PriceKG,ProductPictureURL,ProductDescription,DiscountPercentage)
values('9ed97caf-9e11-400e-8007-1c8134760222','7daefd85-1814-4d7c-9440-24d1db7d85eb','7ff7b7b4-7323-4eef-a29b-22361d873c80','9d590483-53d2-4ef9-8a2d-01d63795daf0','Suseno svinjsko meso',50,1000,'suvomeso Cropped.jpg','Najkvalitetnije domace suseno svinjsko meso',5)
insert into Farm.product(IDProduct,IDProductType,IDClass,IDOrigin,ProductName,SupplyKG,PriceKG,ProductPictureURL,ProductDescription,DiscountPercentage)
values('fde0a9d9-1b9e-4388-a3a2-38031ec8cb15','87f0b9f4-79ff-497a-a8b7-cdd3e2acdc17','7ff7b7b4-7323-4eef-a29b-22361d873c80','9d590483-53d2-4ef9-8a2d-01d63795daf0','Domace kobasice',500,1500,'kobasice-beli-luk Cropped.jpg','Najbolje domace kobasice',0)
insert into Farm.product(IDProduct,IDProductType,IDClass,IDOrigin,ProductName,SupplyKG,PriceKG,ProductPictureURL,ProductDescription,DiscountPercentage)
values('ecc0fd4b-6e03-414e-a1c6-67ed0ed9d600','7daefd85-1814-4d7c-9440-24d1db7d85eb','7ff7b7b4-7323-4eef-a29b-22361d873c80','820d312d-405a-4b86-80ce-45d1515eafc2','Pileci batak',50,250,'Kako-se-cisti-pileci-batak-od-kostiju-2116228616 Cropped Cropped.jpg','Najbolji bataci',10)
insert into Farm.product(IDProduct,IDProductType,IDClass,IDOrigin,ProductName,SupplyKG,PriceKG,ProductPictureURL,ProductDescription,DiscountPercentage)
values('5106d313-5f20-4321-83fa-790fc021a67d','784680bd-c94a-4528-8f50-6241cdb93e9c','7ff7b7b4-7323-4eef-a29b-22361d873c80','18ad44a5-de14-4cee-a1e9-72c965aad067','Kravlje mleko',500,120,'Mk-172-R-web-1024x768 Cropped.jpg','Mleko od krave',0)
insert into Farm.product(IDProduct,IDProductType,IDClass,IDOrigin,ProductName,SupplyKG,PriceKG,ProductPictureURL,ProductDescription,DiscountPercentage)
values('9d40cc8c-e05e-40d6-a0b4-b3eb56d23d4c','87f0b9f4-79ff-497a-a8b7-cdd3e2acdc17','7ff7b7b4-7323-4eef-a29b-22361d873c80','20115872-a12e-4f06-b939-3f2fb54dd9f5','Ajvar',300,1000,'17877-domaci-ajvar_zoom Cropped.jpg','Ajvar od najfinije paprike',20)
insert into Farm.product(IDProduct,IDProductType,IDClass,IDOrigin,ProductName,SupplyKG,PriceKG,ProductPictureURL,ProductDescription,DiscountPercentage)
values('818b33dd-4dc6-44eb-9cab-cd4ca31d20d4','87f0b9f4-79ff-497a-a8b7-cdd3e2acdc17','7ff7b7b4-7323-4eef-a29b-22361d873c80','18ad44a5-de14-4cee-a1e9-72c965aad067','Kravlji sir',50,300,'recept-za-domaci-sir Cropped.jpg','Domaci sir od kravljeg mleka',0)
go

/*insert into Farm.orders(IDOrder,IDUser,TotalOrderPrice,TransactionDate)
values('fa3846a6-4aa1-4ee6-8fd7-bab0e2d0fb73','c471030b-6af8-499a-977d-c006b9efcc95',1000,'2008-11-11')
insert into Farm.orders(IDOrder,IDUser,TotalOrderPrice,TransactionDate)
values('55356a15-181f-4437-883b-9edeb871fb39','c471030b-6af8-499a-977d-c006b9efcc95',1000,'2008-11-11')
go

insert into Farm.order_item(IDOrderItem,IDOrder,IDProduct,OrderAmount,OrderPrice)
values('8232f37c-a039-40a1-8719-464569c39027','fa3846a6-4aa1-4ee6-8fd7-bab0e2d0fb73','9ed97caf-9e11-400e-8007-1c8134760222',10,500)
insert into Farm.order_item(IDOrderItem,IDOrder,IDProduct,OrderAmount,OrderPrice)
values('3b37c507-08e6-4025-96c8-7370fd0f1e0b','fa3846a6-4aa1-4ee6-8fd7-bab0e2d0fb73','9ed97caf-9e11-400e-8007-1c8134760222',10,500)
go

insert into Farm.cart_item(IDCartItem,IDUser,IDProduct,CartAmount,CartPrice)
values('c217b3c7-d7a4-4fb2-8dda-9a2c506518e7','c471030b-6af8-499a-977d-c006b9efcc95','9ed97caf-9e11-400e-8007-1c8134760222',10,500)
insert into Farm.cart_item(IDCartItem,IDUser,IDProduct,CartAmount,CartPrice)
values('fd9db64c-15a1-4cea-a3e4-dc2ee88ee826','c471030b-6af8-499a-977d-c006b9efcc95','9ed97caf-9e11-400e-8007-1c8134760222',10,500)
go

Test za trigger 1
select * from Farm.storage

insert into Farm.stavka_racun(IDRacun,IDProizvod,KolicinaRacun,Cena)
values(1,1,10,500)

select * from Farm.storage

insert into Farm.stavka_racun(IDRacun,IDProizvod,KolicinaRacun,Cena)
values(1,1,10,500)
*/

/* Test za trigger 2
insert into Farm.storage(Kolicina,DatumSkladistenja,RokTrajanja)
values(20,'2008-11-11','2008-11-10')
go
*/