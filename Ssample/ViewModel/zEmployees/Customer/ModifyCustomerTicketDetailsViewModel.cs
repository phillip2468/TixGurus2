using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Syncfusion.UI.Xaml.Grid;

namespace Ssample.ViewModel.zEmployees
{
    public class ModifyCustomerTicketDetailsViewModel : NavigationViewModelBase
    {
        public ICommand NavCommand { get; set; }
        public ModifyCustomerTicketDetailsViewModel()
        {
            AddNewRowPosition = AddNewRowPosition.Top;
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
        }

        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        public AddNewRowPosition AddNewRowPosition { get; set; }
    }
}
