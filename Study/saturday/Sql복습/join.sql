-- inner join
-- p와 b를 조인해서 p의 brand_id와 b의 brand_id가 일치하는 행에
-- 그에 해당하는 b의 brand_name를 추가로 삽입
SELECT p.product_id as 제품번호
	 , p.product_name as 제품명
	 , b.brand_id as 브랜드번호
	 , b.brand_name as 브랜드명
	 , p.category_id as 구분번호
	 , c.category_name as 구분명
	 , p.model_year as 출시년도
	 , CONCAT('$',p.list_price) as 가격 -- 앞에 단위$를 붙임
  FROM Production.Products as p
 INNER JOIN production.brands as b
	ON p.brand_id = b.brand_id
 INNER JOIN production.categories as c
	ON p.category_id = c.category_id
WHERE p.list_price < 999.99

-- outter join
