using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Infrastructure.Design;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Ssample.Model;
using Ssample.ViewModel.Base_view_models;
using Ssample.ViewModel.Buying_tickets;
using Syncfusion.Windows.Shared;

namespace Ssample.ViewModel
{
    /// <summary>
    /// The default view model
    /// </summary>
    public class DefaultViewModel : NavigationViewModelBase
    {
        #region Fields
        private NavigationViewModelBase registerViewModel;

        private NavigationViewModelBase signInViewModel;

        private NavigationViewModelBase viewEventsViewModel;

        private NavigationViewModelBase searchEventViewModel;
        #endregion

        #region Commands
        /// <summary>
        /// Icommand interface for switching between
        /// user controls
        /// </summary>
        public ICommand NavCommand { get; set; }

        public ICommand Nav2Command { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public DefaultViewModel()
        {
            //Initialization
            registerViewModel = new RegisterViewModel();
            viewEventsViewModel = new ViewEventsViewModel();
            signInViewModel = new SignInViewModel();
            searchEventViewModel = new SearchEventViewModel();

            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            Events = (from data in context.Event_Details select data).ToList();

            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
            Nav2Command = new RelayCommand<NavigationViewModelBase>(Nav2);

        }



        /*

        private string SetEventName()
        {
            List<string> eventNameList = new List<string>();
            foreach (var events in Events)
            {
                eventNameList.Add(events);
            }

            var number = eventNameList.Count;
            for (int i = 0; i <= number; i++)
            {
                if (i == 0)
                {
                    return eventNameList.FirstOrDefault();
                }

            }

            return "something went wrong";
        }*/

        #endregion

        #region Helper Functions

        //Navigates to the register view
        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        private void Nav2(NavigationViewModelBase viewModel)
        {
            Properties.Settings.Default.SearchText = SearchText.Trim();
            Navigate(viewModel);
        }
        #endregion


        #region MyRegion
        /// <summary>
        /// Search text
        /// </summary>
        private string _searchText;

        /// <summary>
        /// Property for getting the
        /// search text from the
        /// text box
        /// </summary>
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged($"SearchText");
            }
        }
        /// <summary>
        /// Property of Events
        /// </summary>
        public List<Event_Details> Events { get; set; }

        #endregion

    }
}

