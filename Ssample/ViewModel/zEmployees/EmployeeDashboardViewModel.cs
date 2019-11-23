using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.ViewModel.zEmployees.Event_Management;

namespace Ssample.ViewModel
{
    public class EmployeeDashboardViewModel : NavigationViewModelBase
    {
        private NavigationViewModelBase ModifyCustomerDetailsViewModel;
        private NavigationViewModelBase ModifyEmployeeDetailsViewModel;
        private NavigationViewModelBase GenerateTicketsViewModel;
        private NavigationViewModelBase ModifyEventViewModel;
        private NavigationViewModelBase DataEventsViewModel;


        public ICommand NavCommand { get; set; }
        public ICommand NavLogoutCommand { get; set; }

        public EmployeeDashboardViewModel()
        {
            GenerateTicketsViewModel = new GenerateTicketsViewModel();
            ModifyCustomerDetailsViewModel = new ModifyCustomerDetailsViewModel();
            ModifyEmployeeDetailsViewModel = new ModifyEmployeeDetailsViewModel();
            DataEventsViewModel = new DataEventsViewModel();

            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
            NavLogoutCommand = new RelayCommand<NavigationViewModelBase>(Nav2);
        }

        /// <summary>
        /// Helper function that only is implemented
        /// to log users out.
        /// </summary>
        /// <param name="viewModel"></param>
        private void Nav2(NavigationViewModelBase viewModel)
        {
            Properties.Settings.Default.Email = "guestuser@email.com";
            Navigate(viewModel);
        }

        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }
    }
}
