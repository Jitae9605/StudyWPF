using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMoogaBox.Models
{
	public class Mv_Info
	{
		public string MvName;
		public string Hall;
		public string StartTime;
		public string MvNum;

		public Mv_Info(string mvName, string hall, DateTime startTime, string mvNum)
		{
			MvName = mvName;
			Hall = hall;
			StartTime = startTime.ToString("HH:mm");
			MvNum = mvNum;
		}

		public Mv_Info() { }


	}
}
