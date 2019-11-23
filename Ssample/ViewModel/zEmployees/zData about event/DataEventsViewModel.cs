using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;

namespace Ssample.ViewModel
{
    public class DataEventsViewModel : NavigationViewModelBase
    {
        public ICommand NavCommand { get; set; }
        public DataEventsViewModel()
        {
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
        }

        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }
    }
}
