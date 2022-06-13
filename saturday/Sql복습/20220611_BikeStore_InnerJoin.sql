-- �Ǹ�����
-- stores table�� store_id�� staffs table�� store_id�� ����
	-- ������ ���� �� �������� ��� ���ϴ����� ������ �˼� �ְԵ�

-- �߰��� staffs table�� staff_id�� orders table�� staff_id�� ������
	-- �̸� ���� �������� �߻��� �ֹ����� ��ۻ��¿� ��������� 
	-- !! �����ͺ��̽� ���̾�׷��� ���� JOIN�Ϸ��� ���̺��� ����� Ű���� �� �����������

-- ���⿡ customer talbe�� �����Ͽ� �� �ֹ��� �ֹ����� ����(�̸�, ��ȣ, �ּ� ��)�� ���� ����.

SELECT so.store_id as ������ȣ
     , so.store_name as ������
     , so.phone as ����ȣ
     --, so.email as �̸���
     --, so.street as �Ÿ�
     --, so.city as ����
     --, so.state as ��
     --, so.zip_code as �����ڵ� 
	 , st.staff_id as ��������ȣ
	 , st.first_name + ' ' + st.last_name as ����
	 , st.email as �̸���
	 , st.phone as ����ȣ
	 --, st.active as Ȱ������
	 --, st.store_id as ������ȣ
	 --, st.manager_id as ���ӻ���ȣ
	 , od.order_id
     , od.customer_id
     , CASE WHEN od.order_status = 3 THEN '����غ�'
     		WHEN od.order_status = 4 THEN '��۽���'
			END as '��ۻ���'
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
  FROM sales.stores as so
 INNER JOIN sales.staffs as st
	ON so.store_id = st.store_id
 INNER JOIN sales.orders as od
	ON so.store_id = od.store_id
   AND st.staff_id = od.staff_id
 INNER JOIN sales.customers as cu
	ON cu.customer_id = od.customer_id



-- !!!!!
-- INNER JOIN�� ����� ���� ���� ���� ���� ���̺��� �� ������ ������ ����
-- ������ �߸� §��!..