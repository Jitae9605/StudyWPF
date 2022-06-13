-- OUTER�� INNER�� �ٸ��� �ִ����� ������ �ѱ�� �ִ�.

-- LEFT(������ ����)
SELECT s.[store_cd]
      ,s.[store_addr]
	  ,o.[order_no]
      ,o.[mem_no]
      ,o.[order_date]
      ,o.[store_cd]
  FROM [dbo].[Car_store] as s
 LEFT OUTER JOIN [dbo].[Car_order] as o
    ON s.store_cd = o.store_cd;

-- RIGHT (������ ������ - OUTER JOIN ������)
SELECT s.[store_cd]
      ,s.[store_addr]
	  ,o.[order_no]
      ,o.[mem_no]
      ,o.[order_date]
      ,o.[store_cd]
  FROM [dbo].[Car_store] as s
 RIGHT OUTER JOIN [dbo].[Car_order] as o
    ON s.store_cd = o.store_cd;

