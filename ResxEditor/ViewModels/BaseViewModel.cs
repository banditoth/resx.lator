using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ResxEditor.ViewModels
{
	[INotifyPropertyChanged]
	public partial class BaseViewModel
	{
		public BaseViewModel()
		{

		}

		internal virtual void OnViewAppearing()
        {

        }

		internal virtual void OnViewDisappearing()
        {

        }
	}
}

