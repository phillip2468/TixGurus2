﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleWPF.Navigation;
using SimpleWPF.Navigation.Arguments;
using SimpleWPF.ViewModels;

namespace Ssample.ViewModel
{
    /// <summary>
    /// Handles the navigation of the view models
    /// </summary>
    public class AppViewModel:NavigationViewModelBase,INavigationProvider
    {
        private NavigationViewModelBase current;

        public NavigationViewModelBase Current
        {
            get { return current; }
            set { OnPropertyChanged(ref current, value); }
        }

        private INavigationWindow window;

        public INavigationWindow Window
        {
            get { return window; }
            set { OnPropertyChanged(ref window, value); }
        }

        public AppViewModel()
        {
            service.BeforeNavigation += OnBeforeNavigation;
        }

        private void OnBeforeNavigation(object sender, NavigationEventArgs e)
        {
            Console.WriteLine("About to navigate!");
        }
    }
}
