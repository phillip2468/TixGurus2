using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Ssample.ViewModel.zEmployees.Event_Management
{
    public class ModifyEventsViewModel : NavigationViewModelBase
    {
        #region Fields
        public ICommand NavCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ModifyEventsViewModel()
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
            EventList = (List<Event_Details>)(from data in context.Event_Details select data).ToList();
        }

        #endregion

        #region Helper function
        /// <summary>
        /// A helper function that navigates
        /// </summary>
        /// <param name="viewModel"></param>
        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }


        #endregion

        #region For the datagrid
        /// <summary>
        /// Gets a list
        /// </summary>
        private List<Event_Details> Event { get; set; }

        /// <summary>
        /// Property for list
        /// </summary>
        public List<Event_Details> EventList
        {
            get => Event;
            set => Event = value;
        }

        #endregion
    }
}
