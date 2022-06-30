using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMoogaBox.Models
{
	public class Mv_Info
	{
		public string MvName { get; set; }
		public string Hall { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
		public string MvNum { get; set; }
		public string Seat { get; set; }
		public int MvPrice { get; set; }
		public int Count { get; set; }

		public Mv_Info(string mvName, string hall, DateTime startTime, DateTime runningTime, string mvNum, string seat = "")
		{
			MvName = mvName;
			Hall = hall;
			StartTime = startTime.ToString("HH:mm");
			EndTime = (startTime.AddHours(runningTime.Hour).AddMinutes(runningTime.Minute).ToString("HH:mm"));
			MvNum = mvNum;
			Seat = seat;
			MvPrice = 8000;
			Count = 0;
		}

		public Mv_Info(string mvName, string hall, DateTime startTime, string seat, int count, string mvnum)
		{
			MvName = mvName;
			Hall = hall;
			StartTime = startTime.ToString("HH:mm");
			MvNum = mvnum;
			Seat = seat;
			MvPrice = 8000;
			Count = count;
		}


		public Mv_Info() { }


	}
}
