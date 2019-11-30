using SimpleWPF.ViewModels;
using Ssample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SimpleWPF.Input;
using Ssample.Properties;

namespace Ssample.ViewModel.Buying_tickets
{
    public class PurchaseTicketsViewModel : NavigationViewModelBase
    {
        private NavigationViewModelBase successfulPurchaseViewModel;

        /// <summary>
        /// Command for navigating backwards
        /// </summary>
        public ICommand NavCommand { get; set; }

        public ICommand Nav2Command { get; set; }

        #region Constructor

        /// <summary>
        /// Constructor for purchasing tickets
        /// </summary>
        public PurchaseTicketsViewModel()
        {
            successfulPurchaseViewModel = new SuccessfulPurchaseViewModel();
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
            Nav2Command = new RelayCommand<NavigationViewModelBase>(Nav2);

            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            Tickets = (from data in context.Ticket_Details select data).ToList();

            string seatLocations = Settings.Default.SeatLocation;
            String[] separator = { "," };
            String[] strList = seatLocations.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            foreach (var seatLocation in strList)
            {
                var item = Tickets.FindAll(i => i.seatLocation == seatLocation && i.eventTitle == Settings.Default.EventTitle);
                foreach (var ticketDetails in item) ListOfMatchingTickets.Add(ticketDetails);
            }

        }

        #endregion

        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        private void Nav2(NavigationViewModelBase viewModel)
        {
            if (SaveChanges())
            {
                MessageBox.Show("Successful");
            }
            else
            {
                Navigate(viewModel);
            }
        }

        public List<Ticket_Details> ListOfMatchingTickets { get; set; } = new List<Ticket_Details>();

        public List<Ticket_Details> Tickets { get; set; }

        private Guest_Ticket_Details CurrentGuestTicket { get; set; } = new Guest_Ticket_Details();

        private Guest_Transaction CurrentTransaction { get; set; } = new Guest_Transaction();

        #region Properties


        private decimal _totalPrice;

        public decimal TotalPrice
        {
            get
            {
                if (_totalPrice == 0)
                {
                    _totalPrice = SetTotalPrice() * (decimal)1.15;
                }

                return _totalPrice;
            }
            set => _totalPrice = value;
        }

        private string _email;
        /// <summary>
        /// Property for email
        /// </summary>
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged($"Email");
            }
        }

        private string _fullName;
        /// <summary>
        /// Property for fullname
        /// </summary>
        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged($"FullName");
            }
        }

        private string _address;
        /// <summary>
        /// Property for address
        /// </summary>
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged($"Address");
            }
        }

        #endregion

        #region Helper funtcion

        private decimal SetTotalPrice()
        {
            string seatLocations = Settings.Default.SeatLocation;
            String[] separator = { "," };
            String[] strList = seatLocations.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            decimal total = 0;

            foreach (var seatLocation in strList)
            {
                var item = Tickets.FindAll(i => i.seatLocation == seatLocation && i.eventTitle == Settings.Default.EventTitle);
                foreach (var ticketDetails in item) total += ticketDetails.price;
            }

            return total;
        }

        #endregion


        private bool SaveChanges()
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            foreach (var ticket in ListOfMatchingTickets)
            {
                CurrentGuestTicket.fullName = FullName;
                CurrentGuestTicket.email = Email;
                CurrentGuestTicket.eventAddress = Address;
            }

            return false;
        }

    }
}
