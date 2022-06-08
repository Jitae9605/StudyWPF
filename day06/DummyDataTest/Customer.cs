using System;

namespace DummyDataTest
{
	public class Customer
	{
		public Guid Id { get; set; }	// Guid = 유니크한 아이디로 만들어줌(랜덤, 안겹치게)
		public string Name { get; set; }
		public string Adress { get; set; }
		public string Phone { get; set; }
		public string ContactName { get; set; }
	}
}
