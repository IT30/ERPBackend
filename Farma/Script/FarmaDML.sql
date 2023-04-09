insert into Farm.class(IDClass, Class, ClassDescription)
values ('7ff7b7b4-7323-4eef-a29b-22361d873c80','A', 'Najbolji kvalitet')
insert into Farm.class(IDClass, Class, ClassDescription)
values ('d50a8b84-7754-4a7d-aa3f-68ad5a656d3c','A', 'Najbolji kvalitet')
go

insert into Farm.product_type(IDProductType,Category,CategoryDescription)
values('7daefd85-1814-4d7c-9440-24d1db7d85eb','Svinjski but','Najfinije meso od svinje')
insert into Farm.product_type(IDProductType,Category,CategoryDescription)
values('87f0b9f4-79ff-497a-a8b7-cdd3e2acdc17','Svinjski but','Najfinije meso od svinje')
go

insert into Farm.users(IDUser,ProfilePictureURL, Email, Username, FirstName, LastName, Adress, City, Phone, UserRole, PwdHash, PwdSalt)
values ('c471030b-6af8-499a-977d-c006b9efcc95','abcde', 'email@email.com', 'admin', 'Aca','Peric','Bulevar Oslobodjenja 5','Novi Sad','0624256164','ADMIN','u2soTV8wBLPl2H1gynQC9n7ilfzN5NbEVlAsVvqjX0hR4vF9NRJNpQAskd4cn4196h2613paMARJMSEVyFu7J9iRmqY7jHwGvL2GrxzF2KTAyB5RS6j6OIchTBMNB1EHgowUMDfrlLI96jYDWhktFQYGylEhFg/XBWZHPqtnBC3DSNabM55HLmtyuld3Rrp1ZI7khah79ggQyp0G1b2utfiocvGhw9cBa7GORRlDIgopdEcyXWSW/DRWz1V93fV6qQwqW3C6T9YBW5NdAB49R9pTLQp4cuvtQQGtcQVy+h8q8mO8bGajBwKhFvPlsTf8/W+kS3POGZCyTS4FcS7JAQ==', 'DigOR1g=')
insert into Farm.users(IDUser,ProfilePictureURL, Email, Username, FirstName, LastName, Adress, City, Phone, UserRole, PwdHash, PwdSalt)
values ('b88cd214-35dc-4eb0-a3e3-dd1c25378c66','abcde', 'email', 'user', 'Aca','Peric','Bulevar Oslobodjenja 5','Novi Sad','0624256164','USER','uZMYbuMjfOVgQvb52O17RF+9NxAAGjFVJ1SilBjr4B7psDf8qvIdkl5b8KFW5ymb2iWOF4ZiQYMA6wYN6cljpr9WoVxoAbgMszuBL5j+47VXa49AlchH+YyDp2J16En+w1suUWYpqO1gMM+ayK5mEwdIwvwbgcRuEeSzy8i/tbR0leny37r6inGxuujr62fqnrIqa7ERojz18G8D7mHQjiCvH5uqsnU92/n0M94H6EoyIgK8lsFDn2bMepNEV123a52zAEm9tLEFOI+05gR1WtGax5UF8bN1oLSEpqBbdbkAYswOu+vcIqdZsWRmWq/LlFYVoV5wywNWFOYSY6tXUA==', 'Mw/QITg=')
go

insert into Farm.origin(IDOrigin,OriginName, OriginDescription)
values('9d590483-53d2-4ef9-8a2d-01d63795daf0','Sima','Rasna svinja')
insert into Farm.origin(IDOrigin,OriginName, OriginDescription)
values('18ad44a5-de14-4cee-a1e9-72c965aad067','Sima','Rasna svinja')
go

insert into Farm.product(IDProduct,IDProductType,IDClass,IDOrigin,ProductName,SupplyKG,PriceKG,ProductPictureURL,ProductDescription,DiscountPercentage)
values('9ed97caf-9e11-400e-8007-1c8134760222','7daefd85-1814-4d7c-9440-24d1db7d85eb','7ff7b7b4-7323-4eef-a29b-22361d873c80','9d590483-53d2-4ef9-8a2d-01d63795daf0','Suseno svinjsko meso',50,1000,'abcde','Najkvalitetnije domace suseno svinjsko meso',5)
insert into Farm.product(IDProduct,IDProductType,IDClass,IDOrigin,ProductName,SupplyKG,PriceKG,ProductPictureURL,ProductDescription,DiscountPercentage)
values('ecc0fd4b-6e03-414e-a1c6-67ed0ed9d600','7daefd85-1814-4d7c-9440-24d1db7d85eb','7ff7b7b4-7323-4eef-a29b-22361d873c80','9d590483-53d2-4ef9-8a2d-01d63795daf0','Suseno svinjsko meso',50,1000,'abcde','Najkvalitetnije domace suseno svinjsko meso',5)
go

insert into Farm.orders(IDOrder,IDUser,TotalOrderPrice,TransactionDate)
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

/* Test za trigger 1
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