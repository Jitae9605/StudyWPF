using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMoogaBox.Models
{
	public class Selection
	{
		public string ID { get; set; }
		public string MvName { get; set; }
		public string Hall { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
		public string MvNum { get; set; }
		public string[] SeatNum { get; set; }

		public Selection(Mv_Info mv_Info)
		{
			MvName = mv_Info.MvName;
			Hall = mv_Info.Hall;
			StartTime = mv_Info.StartTime;
			EndTime = mv_Info.EndTime;
			MvNum = mv_Info.MvNum;
		}

		public Selection() { }
	}
}
