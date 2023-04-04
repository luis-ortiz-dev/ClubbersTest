using System;
using System.Collections.Generic;
using PruebaXamarinLuisOrtiz.Services.ServiceLocator;
using PruebaXamarinLuisOrtiz.ViewModels;
using Xamarin.Forms;

namespace PruebaXamarinLuisOrtiz.Views
{	
	public partial class CurrencyPage : ContentPage
	{	
		public CurrencyPage()
		{
			InitializeComponent();

            BindingContext = this.GetViewModel<CurrencyPageViewModel>();
        }
	}
}

