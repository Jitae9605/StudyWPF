-- inner join
-- p�� b�� �����ؼ� p�� brand_id�� b�� brand_id�� ��ġ�ϴ� �࿡
-- �׿� �ش��ϴ� b�� brand_name�� �߰��� ����
SELECT p.product_id as ��ǰ��ȣ
	 , p.product_name as ��ǰ��
	 , b.brand_id as �귣���ȣ
	 , b.brand_name as �귣���
	 , p.category_id as ���й�ȣ
	 , c.category_name as ���и�
	 , p.model_year as ��ó⵵
	 , CONCAT('$',p.list_price) as ���� -- �տ� ����$�� ����
  FROM Production.Products as p
 INNER JOIN production.brands as b
	ON p.brand_id = b.brand_id
 INNER JOIN production.categories as c
	ON p.category_id = c.category_id
WHERE p.list_price < 999.99

-- outter join
