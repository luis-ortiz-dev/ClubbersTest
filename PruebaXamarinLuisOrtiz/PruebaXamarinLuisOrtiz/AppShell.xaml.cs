using System;
using System.Collections.Generic;
using PruebaXamarinLuisOrtiz.ViewModels;
using PruebaXamarinLuisOrtiz.Views;
using Xamarin.Forms;

namespace PruebaXamarinLuisOrtiz
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        //protected override void OnNavigating(ShellNavigatingEventArgs args)
        //{
        //    base.OnNavigating(args);

        //    if (args.Source == ShellNavigationSource.PopToRoot)
        //    {
        //        return;
        //    }

        //    if (args.Source == ShellNavigationSource.ShellSectionChanged)
        //    {
        //        foreach (var item in tab.Items)
        //        {
        //            if (item?.Stack?.Count > 1)
        //            {
        //                item.Stack[1].Navigation.PopToRootAsync();
        //            }
        //        }
        //    }
        //}

    }
}

