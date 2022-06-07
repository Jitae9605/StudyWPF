﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_MVVM_App.ViewModels
{
	public class RelayCommand<T> : ICommand
	{
		private readonly Action<T> excute;  // 실행 처리를 위한 Generic
		private readonly Predicate<T> canExcute; // 실행여부를 판단하는 Generic

		// 실행여부에 따라 이벤트를 추가해주거나 삭제해주는 이벤트핸들러
		public event EventHandler CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
			}
			remove
			{
				CommandManager.RequerySuggested -= value;
			}
		}

		public bool CanExecute(object parameter)
		{
			// ? = ? 앞의 값이 null이면 뒤를 실행
			return canExcute?.Invoke((T)parameter) ?? true;
		}

		public void Execute(object parameter)
		{
			Execute((T)parameter);
		}

		/// <summary>
		/// execute만 파라미터 받는 생성자
		/// </summary>
		/// <param name="execute"></param>
		public RelayCommand(Action<T> execute) : this(execute, null) { }

		/// <summary>
		/// execute, canExecute를 파라미터로 받는 생성자
		/// </summary>
		/// <param name="excute"></param>
		/// <param name="canExcute"></param>
		public RelayCommand(Action<T> excute, Predicate<T> canExcute = null)
		{
			// ?? = ?? 앞의 값이 null이면 ?? 뒤의값으로 대체
			this.excute = excute ?? throw new ArgumentNullException(nameof(excute));
			this.canExcute = canExcute;
		}
	}
}
