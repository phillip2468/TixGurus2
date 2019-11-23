using System.Data.Entity.Infrastructure.Design;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using System.Windows.Input;

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
        #endregion

        #region Commands
        /// <summary>
        /// Icommand interface for switching between
        /// user controls
        /// </summary>
        public ICommand NavCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public DefaultViewModel()
        {
            //Provides instructions on which view to move to
            registerViewModel = new RegisterViewModel();

            signInViewModel = new SignInViewModel();

            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
        }
        #endregion

        #region Helper Functions

        //Navigates to the register view
        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }
        #endregion
    }
}
