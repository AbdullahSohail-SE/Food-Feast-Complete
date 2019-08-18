select * from [order]
select * from order_item

select o.order_id , date from order_item i
join [order] o on
i.order_id=o.order_id
group by o.order_id,date
order by date


insert into payment_method values('Pay with card');

select from payment_method where name=



Create proc Create_order(
@customerId int,
@payment_method varchar(50)
)
As
Insert into [order] 
values (
NEWID(),
@customerId,
(select method_id from payment_method where name=@payment_method),
(select Sum(f.sale_price) as price from cart c join food_item f on c.item_id = f.item_id),
GETDATE() )

Insert into order_item values(SCOPE_IDENTITY() , )

select * from cart

select top 1 item_id from cart where quantity is null;

select * from cart

Exec Create_checkout 7

USE [Food Feast]
GO
select * from cart

Select * from [order] order  by date desc

select * from order_item

select * from customer_address

select * from customer
