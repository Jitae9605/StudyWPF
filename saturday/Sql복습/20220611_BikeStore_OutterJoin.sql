-- OUTER JOIN
SELECT od.order_id
     , od.customer_id
   --, CASE WHEN od.order_status = 3 THEN '����غ�'
   --		WHEN od.order_status = 4 THEN '��۽���'
   --	END as '��ۻ���'
	 , od.order_date as �ֹ���
     , od.required_date as ��������
     , od.shipped_date as ������
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
