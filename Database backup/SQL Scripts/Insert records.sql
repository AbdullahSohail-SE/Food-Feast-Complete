Use [Food Feast];

select * from customer;

insert into customer([name],[email],[phone],[password]) values('Anus Baig','anusbaig08@gmail.com','03343858880','foodwebai');

if('anusbaig08@gmail.com' = (select email from customer where email='anusbaig08@gmail.com'))
insert into review(customer_id,review,date) values((select customer_id from customer where email='anusbaig08@gmail.com'),'Test','Karachi','Your Asp.net restaurant website review contatc system',GetDate());

Alter table review alter column type nvarchar(50) null


select * from review

insert into address values(1,'Johar','Continental','Abdullah','2');
select * from customer join address on customer.customer_id=address.customer_id;

insert into category(name) values('Desi')

select * from category
select * from food_item

insert into category values('Desert');

select * from food_item

insert into food_item(image,name,description,sale_price,category) values('../../Content/media/Images/Menu/Chicken-Shashlik.png','MAPO TOFU','Mapo Tofu is a popular Chinese dish from Sichuan Province, where spicy food is king and the signature spice of the region––the Sichuan Peppercorn––gives dishes a unique “numbing” effect',220,2);

