Create view customer_address
As
select c.customer_id,phone,area,street,building,floor 
from customer c join address a
on c.customer_id=a.customer_id
