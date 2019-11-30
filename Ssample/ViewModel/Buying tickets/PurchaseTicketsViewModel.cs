using SimpleWPF.ViewModels;
using Ssample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using SimpleWPF.Input;
using Ssample.Properties;

namespace Ssample.ViewModel.Buying_tickets
{
    public class PurchaseTicketsViewModel : NavigationViewModelBase
    {
        /// <summary>
        /// Command for navigating backwards
        /// </summary>
        public ICommand NavCommand { get; set; }

        #region Constructor

        /// <summary>
        /// Constructor for purchasing tickets
        /// </summary>
        public PurchaseTicketsViewModel()
        {
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);

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

        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        public List<Ticket_Details> ListOfMatchingTickets { get; set; } = new List<Ticket_Details>();

        public List<Ticket_Details> Tickets { get; set; }

        private Booked_Tickets_Details CurrentTicket { get; set; }

        private List<Event_Details> Event;

        private decimal _totalPrice;

        public decimal TotalPrice
        {
            get
            {
                if (_totalPrice == 0)
                {
                    _totalPrice = SetTotalPrice() * (decimal) 1.15;
                }

                return _totalPrice;
            }
            set => _totalPrice = value;
        }

        #region Properties

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


        private void SaveChanges()
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            Event = (List<Event_Details>)(from data in context.Event_Details select data).ToList();
            
            var rowData = Event.Find(t => t.eventTitle == "The Opera with Snakes");
            
            string seatInput = Properties.Settings.Default.SeatLocation;
            
            string[] separator = { "," };

            string[] stringList = seatInput.Split(separator, 5, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in stringList)
            {
                CurrentTicket.fullName = FullName;
                CurrentTicket.address = Address;
                CurrentTicket.email = Email;
                CurrentTicket.timeStart = rowData.eventStart;
                CurrentTicket.timeEnd = rowData.eventEnd;
                CurrentTicket.seatPlace = s;
            }

           
        }

    }
}
