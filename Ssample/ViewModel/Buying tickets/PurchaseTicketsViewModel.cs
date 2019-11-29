using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleWPF.ViewModels;
using Ssample.Model;

namespace Ssample.ViewModel.Buying_tickets
{
    public class PurchaseTicketsViewModel : NavigationViewModelBase
    {
        public PurchaseTicketsViewModel()
        {
            
        }

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
