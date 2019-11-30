using SimpleWPF.ViewModels;
using Ssample.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ssample.ViewModel.Buying_tickets
{
    public class PurchaseTicketsViewModel : NavigationViewModelBase
    {
        public PurchaseTicketsViewModel()
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            Tickets = (from data in context.Ticket_Details select data).ToList();

            string seatLocations = Properties.Settings.Default.SeatLocation;
            String[] separator = { "," };
            String[] strList = seatLocations.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            foreach (var seatLocation in strList)
            {
                var item = Tickets.FindAll(i => i.seatLocation == seatLocation && i.eventTitle == Properties.Settings.Default.EventTitle);
                foreach (var ticketDetails in item) ListOfMatchingTickets.Add(ticketDetails);
            }
            
        }

        public List<Ticket_Details> ListOfMatchingTickets { get; set; } = new List<Ticket_Details>();

        public List<Ticket_Details> Tickets { get; set; }

        private string _email;
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
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged($"Address");
            }
        }

        private Booked_Tickets_Details CurrentTicket { get; set; }
        private List<Event_Details> Event;

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
