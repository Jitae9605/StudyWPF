-- 판매정보
-- stores table의 store_id와 staffs table의 store_id를 조인
	-- 조인을 통해 각 스태프가 어디서 일하는지를 간단히 알수 있게됨

-- 추가로 staffs table의 staff_id와 orders table의 staff_id를 조인함
	-- 이를 통해 점포별로 발생한 주문들의 배송상태와 배송정보를 
	-- !! 데이터베이스 다이어그램을 보고 JOIN하려는 테이블간의 연결된 키들을 다 연결해줘야함

-- 여기에 customer talbe도 조인하여 각 주문별 주문고객의 정보(이름, 번호, 주소 등)을 같이 띄운다.

SELECT so.store_id as 점포번호
     , so.store_name as 점포명
     , so.phone as 폰번호
     --, so.email as 이메일
     --, so.street as 거리
     --, so.city as 도시
     --, so.state as 주
     --, so.zip_code as 우편코드 
	 , st.staff_id as 스태프번호
	 , st.first_name + ' ' + st.last_name as 성명
	 , st.email as 이메일
	 , st.phone as 폰번호
	 --, st.active as 활동상태
	 --, st.store_id as 점포번호
	 --, st.manager_id as 직속상사번호
	 , od.order_id
     , od.customer_id
     , CASE WHEN od.order_status = 3 THEN '배송준비'
     		WHEN od.order_status = 4 THEN '배송시작'
			END as '배송상태'
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
  FROM sales.stores as so
 INNER JOIN sales.staffs as st
	ON so.store_id = st.store_id
 INNER JOIN sales.orders as od
	ON so.store_id = od.store_id
   AND st.staff_id = od.staff_id
 INNER JOIN sales.customers as cu
	ON cu.customer_id = od.customer_id



-- !!!!!
-- INNER JOIN의 결과는 가장 많은 행을 가진 테이블의 행 갯수를 넘을수 없다
-- 넘으면 잘못 짠것!..