using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;

namespace Ssample.ViewModel
{
    /// <summary>
    /// Sign in view model
    /// </summary>
    public class SignInViewModel : NavigationViewModelBase
    {
        #region Fields

        private NavigationViewModelBase defaultViewModel;
        private NavigationViewModelBase succesfulSignInViewModel;
        private NavigationViewModelBase employeeLoginViewModel;

        #endregion

        #region Commands
        public ICommand GoToEmployeeViewCommand { get; set; }
        //Go back command
        public ICommand NavCommand { get; set; }
        public ICommand GoToSuccessfulSignInViewCommand { get; set; }

        #endregion

        #region Constructor

        public SignInViewModel()
        {
            employeeLoginViewModel = new EmployeeLoginViewModel();

            //Assigns the successful sign in
            succesfulSignInViewModel = new SuccessfulSignInViewModel();

            //Command to go to the successful sign in page
            GoToSuccessfulSignInViewCommand = new RelayCommand(GoToSuccessfulSignIn);

            GoToEmployeeViewCommand = new RelayCommand(GoToEmployeeLogin);

            //Command for going back
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
        }

        #endregion

        #region Helper functions

        /// <summary>
        /// A function that checks whether entered in values
        /// corresponds to those in database. If false, returns
        /// a message box indicating to user it has been blocked
        /// </summary>
        private void GoToSuccessfulSignIn()
        {
            if (CanSignIn(Email, Password))
            {
                Navigate(succesfulSignInViewModel);
            }
            else
            {
                MessageBox.Show("You have entered the wrong sign in values");
            }
        }
        /// <summary>
        /// Functionality to go back
        /// </summary>
        /// <param name="viewModel"></param>
        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        private void GoToEmployeeLogin()
        {
            Navigate(employeeLoginViewModel);
        }
        #endregion

        #region Login fields

        public Customer_Details CurrentCustomer { get; set; } = new Customer_Details(); //Current customer you need to edit

        #endregion

        #region Fields for sign in

        /// <summary>
        /// Field for email
        /// </summary>
        public string Email
        {
            get => CurrentCustomer.email;
            set
            {
                CurrentCustomer.email = value;
                OnPropertyChanged($"Email");
            }
        }

        /// <summary>
        /// Field for password
        /// </summary>
        public string Password
        {
            get => CurrentCustomer.password;
            set
            {
                CurrentCustomer.password = value;
                OnPropertyChanged($"Password");
            }
        }
        #endregion

        #region Function for signing in
        /// <summary>
        /// Function which checks if values entered are in the database
        /// </summary>
        /// <param name="email">The email property</param>
        /// <param name="password">The password property</param>
        /// <returns></returns>
        public bool CanSignIn(string email, string password)
        {
            //Declares a data context
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            bool output = false;

            //Checks for null values
            var user = context.Customer_Details.FirstOrDefault(u => u.email == Email);

            //Checks if the entered in values are longer than 0
            if (email?.Length > 0 && password?.Length > 0 && user != null)
            {
                //If an entry equals a values in the database return true
                if (user.password == Password)
                {
                    Properties.Settings.Default.Email = CurrentCustomer.email;
                    Properties.Settings.Default.Save();
                    return true;
                }
            }
            //Returns false if sign in failed
            context.Dispose();
            return output;
        }

        #endregion
    }
}
