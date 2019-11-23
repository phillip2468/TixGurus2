﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;

namespace Ssample.ViewModel.zEmployees
{
    public class AddCustomerDetailsViewModel : NavigationViewModelBase
    {
        public ICommand NavCommand { get; set; }
        public AddCustomerDetailsViewModel()
        {
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            CustomerDetails = (List<Customer_Details>)(from data in context.Customer_Details select data).ToList();
        }

        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        private List<Customer_Details> Customer { get; set; }

        public List<Customer_Details> CustomerDetails
        {
            get { return Customer; }
            set { Customer = value; }
        }
    }
}
