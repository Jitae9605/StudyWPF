﻿using Caliburn.Micro;
using OxyPlot;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using WpfSmartHomeMonitoringApp.Helpers;
using WpfSmartHomeMonitoringApp.Models;

namespace WpfSmartHomeMonitoringApp.ViewModels
{
	public class HistoryViewModel : Screen
	{
		private BindableCollection<DivisionModel> divisions;
		private DivisionModel selectedDivision;
		private string initStartDate;
		private string startDate;
		private string endDate;
		private string initEndDate;
		private int totalCount;
		private PlotModel smartHomeModel;

		public BindableCollection<DivisionModel> Divisions
		{
			get => divisions;
			set
			{
				divisions = value;
				NotifyOfPropertyChange(() => Divisions);
			}
		}

		public DivisionModel SelectedDivision
		{
			get => selectedDivision; 
			set
			{
				selectedDivision = value;
				NotifyOfPropertyChange(() => SelectedDivision);
			}
		}

		public string InitStartDate
		{
			get => initStartDate;
			set
			{
				initStartDate = value;
				NotifyOfPropertyChange(() => InitStartDate);
			}
		}

		public string StartDate
		{
			get => startDate;
			set
			{
				startDate = value;
				NotifyOfPropertyChange(() => StartDate);
			}
		}

		public string EndDate
		{
			get => endDate;
			set
			{
				endDate = value;
				NotifyOfPropertyChange(() => EndDate);
			}
		}

		public string InitEndDate
		{
			get => initEndDate;
			set
			{
				initEndDate = value;
				NotifyOfPropertyChange(() => InitEndDate);
			}
		}

		public int TotalCount
		{
			get => totalCount;
			set
			{
				totalCount = value;
				NotifyOfPropertyChange(() => TotalCount);
			}
		}

		public PlotModel SmartHomeModel
		{
			get => smartHomeModel;
			set
			{
				smartHomeModel = value;
				NotifyOfPropertyChange(() => SmartHomeModel);
			}
		}

		public HistoryViewModel()
		{
			Commons.CONNSTRING = "Data Source=PC01;Initial Catalog=OpenApiLab;Integrated Security=True";
			InitControl();
		}

		/// <summary>
		/// 로드되자마자 들어갈 데이터생성 및 입력(콤보박스, 기본 디폴트 날짜)
		/// </summary>
		private void InitControl()
		{
			Divisions = new BindableCollection<DivisionModel>	// 콤보박스용 데이터 생성
			{
				new DivisionModel { KeyVal = 0, DivisionVal = "-- Select --"},
				new DivisionModel { KeyVal = 1, DivisionVal = "DINNING"},
				new DivisionModel { KeyVal = 2, DivisionVal = "LIVING"},
				new DivisionModel { KeyVal = 3, DivisionVal = "BED"},
				new DivisionModel { KeyVal = 4, DivisionVal = "BATH"}
			};

			// select를 포함하는 것을 디폴트 또는 첫번째로 선택해서 초기화
			SelectedDivision = Divisions.Where(v => v.DivisionVal.Contains("Select")).FirstOrDefault();

			InitStartDate = DateTime.Now.ToShortDateString();			// 2022-06-10
			InitEndDate = DateTime.Now.AddDays(1).ToShortDateString();	// 2022-06-11
		}

		// 검색 메서드
		public void SearchIoTData()
		{
			// Validation Check (유효성 검증)
			if (SelectedDivision.KeyVal == 0)   // Select 일때
			{
				// 검색 안되야함
				MessageBox.Show("검색할 방을 선택하세요.");
				return;
			}

			if(DateTime.Parse(StartDate) > DateTime.Parse(EndDate))	// 시작일이 종료일보다 미래일때
			{
				MessageBox.Show("시작일이 종료일보다 최신일 수 없습니다.");
				return;
			}

			TotalCount = 0;

			using (SqlConnection conn = new SqlConnection(Commons.CONNSTRING))
			{
				string strQuery = @"SELECT Id, CurrTime, Temp, Humid
										FROM TblSmartHome
									WHERE DevId = @DevId
										AND CurrTime BETWEEN @StartDate AND @EndDate
									ORDER BY Id ASC ";
				try
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand(strQuery, conn);

					SqlParameter parmDevId = new SqlParameter("DevId", SelectedDivision.DivisionVal);
					cmd.Parameters.Add(parmDevId);

					SqlParameter parmStartDate = new SqlParameter("@StartDate", StartDate);
					cmd.Parameters.Add(parmStartDate);

					SqlParameter parmEndDate = new SqlParameter("@EndDate", EndDate);
					cmd.Parameters.Add(parmEndDate);

					SqlDataReader reader = cmd.ExecuteReader();

					var i = 0;
					while (reader.Read())
					{
						var Tmep = reader["Temp"];
						// Temp,Humid 차트데이터 생성


						i++;
					}

					TotalCount = i;
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Error {ex.Message}");
					return;
				}
			}
		}
	}
}
