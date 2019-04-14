select CUSTOMER_ID, max(counter)  from (select CUSTOMER_ID, count(*) as counter from ORDERS group by Customer_ID)

select CUSTOMER_ID, sum(PRICE) from (select * from ORDERS AS o join Products AS p on o.PRODUCT_ID = p.ID) group by CUSTOMER_ID
