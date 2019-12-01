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

            string guestTicketId = Settings.Default.guestTicketId;
            String[] separator = { "," };
            string[] guestTicketList = guestTicketId.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            foreach (var ticketId in guestTicketList)
            {
                var rowData = (from data in context.Guest_Ticket_Details where data.ticketId.ToString().Contains(ticketId) && data.email.Contains(Settings.Default.guestEmail) select data).ToList();
                foreach (var guestTicketDetails in rowData) GuestTicketDetails.Add(guestTicketDetails);
            }


            GuestTransactionsDetails = (from data in context.Guest_Transaction
                where data.transactionId.ToString().Contains(Properties.Settings.Default.guestTransactionId)
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
