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

namespace Ssample.ViewModel.zEmployees.Event_Management
{
    public class GenerateTicketsViewModel : NavigationViewModelBase
    {
        private NavigationViewModelBase successfulTicketsViewModel;

        public ICommand GoToSucCommand { get; set; }

        public GenerateTicketsViewModel()
        {
            successfulTicketsViewModel = new SuccessfulTicketsViewModel();
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            EventList = (List<Event_Details>)(from data in context.Event_Details select data).ToList();
            GoToSucCommand = new RelayCommand(Navigate);
        }

        public Ticket_Details CurrentTicket = new Ticket_Details();

        private void Navigate()
        {
            if (SaveChanges())
            {
                Navigate(successfulTicketsViewModel);
            }
            MessageBox.Show("Something went wrong");
        }
        #region Properties
        public string EventTitle
        {
            get
            {
                if (CurrentTicket.EventTitle == null)
                {
                    return CurrentTicket.EventTitle;
                }

                return CurrentTicket.EventTitle;
            }
            set
            {
                CurrentTicket.EventTitle = value;
                OnPropertyChanged($"EventTitle");
            }
        }

        public string LevelOneChar
        {
            get
            {
                if (CurrentTicket.SeatLetter == null)
                {
                    return CurrentTicket.SeatLetter;
                }

                return CurrentTicket.SeatLetter;
            }
            set
            {
                CurrentTicket.SeatLetter = value;
                OnPropertyChanged($"LevelOneChar");
            }
        }

        public decimal LevelOnePrice
        {
            get
            {
                if (CurrentTicket.Price == 0)
                {
                    return CurrentTicket.Price;
                }

                return CurrentTicket.Price;
            }
            set
            {
                CurrentTicket.Price = value;
                OnPropertyChanged($"LevelOnePrice");
            }
        }

        private int _levelOneCapacity;
        /// <summary>
        /// Gets the amount of tickets
        /// </summary>
        public int LevelOneCapacity
        {
            get
            {
                if (_levelOneCapacity == 0)
                {
                    return _levelOneCapacity;
                }

                return _levelOneCapacity;
            }
            set
            {
                _levelOneCapacity = value;
                OnPropertyChanged($"LevelOneCapacity");
            }
        }

        public DateTime EventStartLevelOne
        {
            get
            {
                if (CurrentTicket.EventStart == DateTime.MinValue)
                {
                    return CurrentTicket.EventStart;
                }

                return CurrentTicket.EventStart;
            }
            set
            {
                CurrentTicket.EventStart = value;
                OnPropertyChanged($"EventStart");
            }
        }
        public DateTime EventEndLevelOne
        {
            get
            {
                if (CurrentTicket.EventEnd == DateTime.MinValue)
                {
                    return CurrentTicket.EventEnd;
                }

                return CurrentTicket.EventEnd;
            }
            set
            {
                CurrentTicket.EventEnd = value;
                OnPropertyChanged($"EventEnd");
            }
        }

        #endregion


        private bool SaveChanges()
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            int numberOfTickets = LevelOneCapacity;

            bool output = false;

            for (int i = 0; i < numberOfTickets; i++)
            {
                CurrentTicket.EventTitle = EventTitle;
                CurrentTicket.Price = LevelOnePrice;
                CurrentTicket.SeatLetter = LevelOneChar;
                CurrentTicket.SeatNumber = i;
                CurrentTicket.EventStart = EventStartLevelOne;
                CurrentTicket.EventEnd = EventEndLevelOne;
                context.Ticket_Details.Add(CurrentTicket);

                context.SaveChanges();
                output = true;
            }

            return output;
        }

        #region For datagrid
        private List<Event_Details> Event { get; set; }

        public List<Event_Details> EventList
        {
            get => Event;
            set => Event = value;
        }

        #endregion

    }
}
