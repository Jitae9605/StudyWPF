-- OUTER는 INNER와 다르게 최대행의 갯수를 넘길수 있다.

-- LEFT(기준이 왼쪽)
SELECT s.[store_cd]
      ,s.[store_addr]
	  ,o.[order_no]
      ,o.[mem_no]
      ,o.[order_date]
      ,o.[store_cd]
  FROM [dbo].[Car_store] as s
 LEFT OUTER JOIN [dbo].[Car_order] as o
    ON s.store_cd = o.store_cd;

-- RIGHT (기준이 오른쪽 - OUTER JOIN 글자의)
SELECT s.[store_cd]
      ,s.[store_addr]
	  ,o.[order_no]
      ,o.[mem_no]
      ,o.[order_date]
      ,o.[store_cd]
  FROM [dbo].[Car_store] as s
 RIGHT OUTER JOIN [dbo].[Car_order] as o
    ON s.store_cd = o.store_cd;

