using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM_App.Helpers;

namespace WPF_MVVM_App.Models
{
	public class Person
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		private string email;

		public string Email
		{
			get { return email; } // == get => email;

			set
			{
				if (Commons.IsValidEmail(email))
					throw new Exception("Invalid Email");
				else
					email = value;
			}
		}

		public DateTime date;
		public DateTime Date 
		{	
			get { return date; }
			set
			{
				var result = Commons.CalcAge(value);
				if (result > 135 || result < 0)
					throw new Exception("Invalid Date");
				else
					date = value;
			}
		}
		public bool IsBirthDay 
		{
			get
			{
				return DateTime.Now.Month == date.Month && DateTime.Now.Day == date.Day;
			}
		}
		public bool IsAdult { get; }

		public Person(string firstName, string lastName, string email, DateTime date)
		{
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			Date = date;
		}
	}
}
