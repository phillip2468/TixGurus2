using System.Windows.Input;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Syncfusion.UI.Xaml.Grid;

namespace Ssample.ViewModel.zEmployees.Customer
{
    /// <summary>
    /// A class responsible for modifying the ticket details
    /// </summary>
    public class ModifyCustomerTicketDetailsViewModel : NavigationViewModelBase
    {
        #region Commands

        /// <summary>
        /// A command responsible for navigation
        /// </summary>
        public ICommand NavCommand { get; set; }

        #endregion

        #region Constructor

        public ModifyCustomerTicketDetailsViewModel()
        {
            AddNewRowPosition = AddNewRowPosition.Top;
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
        }

        #endregion

        #region Helper functions
        /// <summary>
        /// A function which navigates to the view model
        /// given by its argument
        /// </summary>
        /// <param name="viewModel">The view model</param>
        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        public AddNewRowPosition AddNewRowPosition { get; set; }

        #endregion
    }
}
