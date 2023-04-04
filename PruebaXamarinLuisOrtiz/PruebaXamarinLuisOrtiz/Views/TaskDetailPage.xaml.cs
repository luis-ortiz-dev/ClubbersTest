using System;
using System.Collections.Generic;
using PruebaXamarinLuisOrtiz.Services.ServiceLocator;
using PruebaXamarinLuisOrtiz.ViewModels;
using Xamarin.Forms;

namespace PruebaXamarinLuisOrtiz.Views
{	
	public partial class TaskDetailPage : ContentPage
	{	
		public TaskDetailPage ()
		{
            InitializeComponent();

            BindingContext = this.GetViewModel<TaskDetailPageViewModel>();
        }
	}
}

