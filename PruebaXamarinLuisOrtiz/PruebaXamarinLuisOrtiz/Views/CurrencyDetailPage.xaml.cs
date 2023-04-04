using System;
using System.Collections.Generic;
using PruebaXamarinLuisOrtiz.Services.ServiceLocator;
using PruebaXamarinLuisOrtiz.ViewModels;
using Xamarin.Forms;
namespace PruebaXamarinLuisOrtiz.Views
{	
	public partial class CurrencyDetailPage : ContentPage
	{	
		public CurrencyDetailPage()
		{
			InitializeComponent();

            BindingContext = this.GetViewModel<CurrencyDetailPageViewModel>();
        }
	}
}

