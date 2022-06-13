-- OUTER JOIN
SELECT od.order_id
     , od.customer_id
   --, CASE WHEN od.order_status = 3 THEN '배송준비'
   --		WHEN od.order_status = 4 THEN '배송시작'
   --	END as '배송상태'
	 , od.order_date as 주문일
     , od.required_date as 예상도착일
     , od.shipped_date as 도착일
   --, od.store_id
   --, od.staff_id
	 , cu.customer_id
     , cu.first_name
     , cu.last_name
     , cu.phone
     , cu.email
     , cu.street
     , cu.city
     , cu.state
     , cu.zip_code
  FROM sales.orders as od
 RIGHT OUTER JOIN sales.customers as cu
	ON cu.customer_id = od.customer_id
