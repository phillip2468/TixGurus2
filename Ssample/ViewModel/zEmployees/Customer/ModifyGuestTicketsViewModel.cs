using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;

namespace Ssample.ViewModel.zEmployees.Customer
{
    public class ModifyGuestTicketsViewModel : NavigationViewModelBase
    {
        public ICommand NavCommand { get; set; }
        public ModifyGuestTicketsViewModel()
        {
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
        }

        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }
    }
}
