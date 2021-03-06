﻿using Ssample.Model;
using Ssample.Properties;
using Ssample.ViewModel;
using Ssample.ViewModel.Dashboard;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Ssample.Views.Dashboard
{
    /// <summary>
    /// Interaction logic for ChangeMyDetailsView.xaml
    /// </summary>
    public partial class ChangeMyDetailsView : UserControl
    {
        //Initilization of the data entities
        CustomerDatabaseEntities dataEntities = new CustomerDatabaseEntities();
        public ChangeMyDetailsView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// On user control loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Assigns the data context to the change details
            DataContext = new ChangeMyDetailsViewModel(new DefaultViewModel());

            //First initialization of the data shown;
            //This populates the text fields with
            //data about the customer from the database
        }
    }
}
