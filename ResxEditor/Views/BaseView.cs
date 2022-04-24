using System;
using ResxEditor.ViewModels;

namespace ResxEditor.Views
{
	public class BaseView : ContentPage
	{
		public BaseView()
		{

		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(BindingContext is BaseViewModel vm)
            {
                vm.OnViewAppearing();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if(BindingContext is BaseViewModel vm)
            {
                vm.OnViewDisappearing();
            }
        }
    }
}

