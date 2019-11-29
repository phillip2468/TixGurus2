using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;

namespace Ssample.ViewModel.Base_view_models
{
    public class SearchEventViewModel : NavigationViewModelBase
    {

        public ICommand NavCommand { get; set; }
        public SearchEventViewModel()
        {
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
        }

        private void Nav(NavigationViewModelBase viewModel)
        {
            Properties.Settings.Default.SearchText = "";
            Navigate(viewModel);
        }

        private string _textOfSearch;

        public string TextOfSearch
        {
            get { return _textOfSearch = Properties.Settings.Default.SearchText; }
            set { _textOfSearch = value; }
        }
    }
}
