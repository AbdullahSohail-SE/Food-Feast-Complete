Use [Food Feast];

Alter proc select_login_customer(
@phone nvarchar(20))
As
select * from customer where phone=@phone;
Go

Alter proc select_login_customer_from_id(
@id int)
As
select phone,area,street,building,floor,customer.customer_id from address join customer
on address.customer_id=customer.customer_id where customer.customer_id=@id;
Go

Alter proc select_customer_email_from_id(@customerID int)
As
select email from customer where customer_id=@customerID;

Alter proc update_customer_address(
@customerId int,
@area varchar(50),
@street varchar(50),
@building varchar(50),
@floor int,
@phone varchar(20)
)
As
update address  set 
building=ISNULL(@building,(select building from customer_address where customer_id=@customerId)) ,
street=ISNULL(@street,(select street from customer_address where customer_id=@customerId)) ,
area=ISNULL(@area,(select area from customer_address where customer_id=@customerId)) ,
floor=ISNULL(@floor,(select floor from customer_address where customer_id=@customerId))
where customer_id=@customerId

update customer set 
phone=ISNULL(@phone,(select phone from customer_address where customer_id=@customerId))
where customer_id=@customerId


Alter proc insert_customer_review(
@email nvarchar(50),
@city nvarchar(50),
@type nvarchar(50),
@message nvarchar(250)
)
As
if(@email = (select email from customer where email=@email))
insert into review(customer_id,city,type,review,date) values((select customer_id from customer where email=@email),@city,@type,@message,GetDate());
Go

Alter proc [get_category_name_with_items_count]
As
select c.name , COUNT(*) , c.category_id as items from food_item f join category c
on f.category=c.category_id
group by c.category_id , c.name
order by c.category_id
Go

Alter proc [select_fooditem_with_category]
As
select * from food_item join category 
on food_item.category = category.category_id
order by category
Go

Alter proc get_count_items
As
select COUNT(*) from food_item
Go

Alter proc insert_cart(
@item int
)
As
insert into cart(item_id) values(@item);
Go

Alter proc [items_from_cart]
As
select c.item_id,f.name,f.sale_price * ISNULL(c.quantity,1) ,c.quantity from cart c join food_item f
on c.item_id=f.item_id;
GO

Alter proc Create_order(
@customerId int,
@payment_method varchar(50)
)
As
Declare @order_id uniqueidentifier;
Set @order_id=NEWID();
Insert into [order] 
values (
@order_id,
@customerId,
(select method_id from payment_method where name=@payment_method),
(select Sum(c.price) as price from cart c),
GETDATE() )
Insert into order_item(order_id,item_id,quantity,price) select @order_id,item_id,quantity,price  from cart;
Go

Alter proc Create_checkout(@quantity int)
As
update cart set 
quantity=@quantity,
price=(select sale_price from food_item where item_id=(select top 1 item_id from cart where quantity is null)) * @quantity
where item_id=(select top 1 item_id from cart where quantity is null);
Go


