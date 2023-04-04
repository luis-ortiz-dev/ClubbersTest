using System;
using System.Collections.Generic;
using PruebaXamarinLuisOrtiz.Services.ServiceLocator;
using PruebaXamarinLuisOrtiz.ViewModels;
using Xamarin.Forms;

namespace PruebaXamarinLuisOrtiz.Views
{	
	public partial class TaskPage : ContentPage
	{	
		public TaskPage ()
		{
			InitializeComponent ();

            BindingContext = this.GetViewModel<TaskPageViewModel>();
        }
	}
}

