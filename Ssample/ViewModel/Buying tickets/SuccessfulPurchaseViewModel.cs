using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using Ssample.Properties;

namespace Ssample.ViewModel.Buying_tickets
{
    public class SuccessfulPurchaseViewModel : NavigationViewModelBase
    {

        public ICommand NavCommand { get; set; }
        public SuccessfulPurchaseViewModel()
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            string seatLocations = Settings.Default.SeatLocation;
            String[] separator = { "," };
            String[] strList = seatLocations.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            foreach (var seatLocation in strList)
            {
                var rowData = (from data in context.Guest_Ticket_Details where data.seatLocation.Contains(seatLocation) select data).ToList();
                foreach (var guestTicketDetails in rowData) GuestTicketDetails.Add(guestTicketDetails);
            }

            GuestTransactionsDetails = (from data in context.Guest_Transaction
                                        where data.email.Contains(Properties.Settings.Default.guestEmail)
                                        select data).ToList();

            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
        }

        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }


        public List<Guest_Ticket_Details> GuestTicketDetails { get; set; } = new List<Guest_Ticket_Details>();

        public List<Guest_Transaction> GuestTransactionsDetails { get; set; } = new List<Guest_Transaction>();
    }
}
