using SimpleWPF.ViewModels;
using Ssample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        #region Helper functions

        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        private void Nav2(NavigationViewModelBase viewModel)
        {
            if (SaveChanges())
            {
                MessageBox.Show("Successful");
                Navigate(viewModel);
            }
            else
            {
                MessageBox.Show("Something went wrong");
            }
        }

        #endregion

        public List<Ticket_Details> ListOfMatchingTickets { get; set; } = new List<Ticket_Details>();

        public List<Ticket_Details> Tickets { get; set; }

        public List<Event_Details> Events { get; set; } = new List<Event_Details>();

        public List<Event_Details> ListOfMatchingEvents { get; set; } = new List<Event_Details>();

        private Guest_Ticket_Details CurrentGuestTicket { get; set; } = new Guest_Ticket_Details();

        private Guest_Transaction CurrentTransaction { get; set; } = new Guest_Transaction();

        #region Properties


        private decimal _totalPrice;
        /// <summary>
        /// A property for the total price. It takes into account an
        /// 15 % fee for the total
        /// </summary>
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

        /// <summary>
        /// Property for email
        /// </summary>
        public string Email
        {
            get
            {
                if (CurrentGuestTicket.email == null)
                {
                    return CurrentGuestTicket.email;
                }
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(CurrentGuestTicket.email);
                if (!match.Success)
                {
                    return null;
                }
                return CurrentGuestTicket.email;
            }
            set
            {
                CurrentGuestTicket.email = value;
                OnPropertyChanged($"Email");
            }
        }

        private string _firstName;
        /// <summary>
        /// A property for the first name
        /// </summary>
        public string FirstName
        {
            get
            {
                if (_firstName == null)
                {
                    return _firstName;
                }
                if (_firstName.Length < 3)
                {
                    return null;
                }
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged($"FirstName");
            }
        }

        private string _lastName;
        /// <summary>
        /// A property for the last name
        /// </summary>
        public string LastName
        {
            get
            {
                if (_lastName == null)
                {
                    return _lastName;
                }
                if (_lastName.Length < 3)
                {
                    return null;
                }
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged($"LastName");
            }
        }

        /// <summary>
        /// Property for address
        /// </summary>
        public string Address
        {
            get {
                if (CurrentTransaction.address == null)
                {
                    return CurrentTransaction.address; 
                }
                Regex regex = new Regex(@"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$");
                Match match = regex.Match(CurrentTransaction.address);
                if (!match.Success)
                {
                    return null;
                }

                return CurrentTransaction.address;
            }
            set
            {
                CurrentTransaction.address = value;
                OnPropertyChanged($"Address");
            }
        }

        private string _eventAddress;

        /// <summary>
        /// This property returns the address of the location
        /// </summary>
        public string EventAddress
        {
            get
            {
                if (_eventAddress != null) return _eventAddress;
                foreach (var item in ListOfMatchingEvents)
                {
                    var eventAddress = item.eventAddress;
                    _eventAddress = eventAddress;
                    return _eventAddress;
                }

                return _eventAddress;
            }
            set => _eventAddress = value;
        }

        private string _placeName;
        /// <summary>
        /// This property returns the name of the location e.g.
        /// Louis Events Museum
        /// </summary>
        public string PlaceName
        {
            get
            {
                if (_placeName != null) return _placeName;
                foreach (var item in ListOfMatchingEvents)
                {
                    var placeName = item.eventLocation;
                    _placeName = placeName;
                    return _placeName;
                }

                return _placeName;
            }
            set => _placeName = value;
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
            bool output = false;

            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            Events = (from data in context.Event_Details select data).ToList();

            foreach (var eventsEvent in Events)
            {
                var item = Events.FindAll(i => i.eventTitle == Properties.Settings.Default.EventTitle);
                foreach (var matchingEvent in item) ListOfMatchingEvents.Add(matchingEvent);
            }

            bool isTransactionNull = ArePropertiesNull(CurrentTransaction);
            bool isGuestNull = ArePropertiesNull(CurrentGuestTicket);

            if (!isGuestNull)
            {
                foreach (var ticket in ListOfMatchingTickets)
                {
                    CurrentGuestTicket.fullName = FirstName.Trim() + " " + LastName.Trim();
                    CurrentGuestTicket.email = Email.Trim();
                    CurrentGuestTicket.price = ticket.price;
                    CurrentGuestTicket.timeStart = ticket.eventStart;
                    CurrentGuestTicket.timeEnd = ticket.eventEnd;
                    CurrentGuestTicket.seatLocation = ticket.seatLocation.Trim();
                    CurrentGuestTicket.eventName = Settings.Default.EventTitle;
                    CurrentGuestTicket.eventAddress = EventAddress.Trim();
                    CurrentGuestTicket.placeName = PlaceName.Trim();
                    context.Guest_Ticket_Details.Add(CurrentGuestTicket);
                    context.SaveChanges();
                }
            }

            if (!isTransactionNull)
            {
                CurrentTransaction.email = Email.Trim();
                CurrentTransaction.address = Address.Trim();
                CurrentTransaction.eventAddress = EventAddress.Trim();
                CurrentTransaction.eventName = Properties.Settings.Default.EventTitle;
                CurrentTransaction.totalPrice = TotalPrice;
                context.Guest_Transaction.Add(CurrentTransaction);
                context.SaveChanges();
                output = true;
                return output;
            }

            return output;
        }

        #region Checking for null
        /// <summary>
        /// A function which checks whether the select object is
        /// null or not null
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="obj">The object itself</param>
        /// <returns>A boolean value</returns>
        public bool ArePropertiesNull<T>(T obj)
        {
            return typeof(T).GetProperties().All(propertyInfo => propertyInfo.GetValue(obj) != null);
        }

        #endregion

    }
}
