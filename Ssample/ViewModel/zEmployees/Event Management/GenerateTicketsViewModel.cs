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
        public ICommand NavCommand { get; set; }

        public GenerateTicketsViewModel()
        {
            successfulTicketsViewModel = new SuccessfulTicketsViewModel();
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            EventList = (List<Event_Details>)(from data in context.Event_Details select data).ToList();
            GoToSucCommand = new RelayCommand(Navigate);
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
        }

        public Ticket_Details CurrentTicket = new Ticket_Details();

        private void Navigate()
        {
            if (SaveChanges())
            {
                Navigate(successfulTicketsViewModel);
            }
            else
            {
                MessageBox.Show("Something went wrong");
            }
        }

        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }
        #region Properties

        #region Level One Properties

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

        #region Level Two properties
        private string _LevelTwoChar;
        public string LevelTwoChar
        {
            get
            {
                if (_LevelTwoChar == null)
                {
                    return _LevelTwoChar;
                }

                return _LevelTwoChar;
            }
            set
            {
                _LevelTwoChar = value;
                OnPropertyChanged($"LevelTwoChar");
            }
        }

        private decimal _levelTwoPrice;
        public decimal LevelTwoPrice
        {
            get
            {
                if (_levelTwoPrice == 0)
                {
                    return _levelTwoPrice;
                }

                return _levelTwoPrice;
            }
            set
            {
                _levelTwoPrice = value;
                OnPropertyChanged($"LevelTwoPrice");
            }
        }

        private int _levelTwoCapacity;
        /// <summary>
        /// Gets the amount of tickets
        /// </summary>
        public int LevelTwoCapacity
        {
            get
            {
                if (_levelTwoCapacity == 0)
                {
                    return _levelTwoCapacity;
                }

                return _levelTwoCapacity;
            }
            set
            {
                _levelTwoCapacity = value;
                OnPropertyChanged($"LevelTwoCapacity");
            }
        }
        #endregion

        #region Level Three Properties
        private string _LevelThreeChar;
        public string LevelThreeChar
        {
            get
            {
                if (_LevelThreeChar == null)
                {
                    return _LevelThreeChar;
                }

                return _LevelThreeChar;
            }
            set
            {
                _LevelThreeChar = value;
                OnPropertyChanged($"LevelThreeChar");
            }
        }

        private decimal _levelThreePrice;
        public decimal LevelThreePrice
        {
            get
            {
                if (_levelThreePrice == 0)
                {
                    return _levelTwoPrice;
                }

                return _levelThreePrice;
            }
            set
            {
                _levelThreePrice = value;
                OnPropertyChanged($"LevelThreePrice");
            }
        }

        private int _levelThreeCapacity;
        /// <summary>
        /// Gets the amount of tickets
        /// </summary>
        public int LevelThreeCapacity
        {
            get
            {
                if (_levelTwoCapacity == 0)
                {
                    return _levelThreeCapacity;
                }

                return _levelThreeCapacity;
            }
            set
            {
                _levelThreeCapacity = value;
                OnPropertyChanged($"LevelThreeCapacity");
            }
        }


        #endregion

        #endregion

        #region Save changes to database
        private bool SaveChanges()
        {

            int numberOfTickets = LevelOneCapacity;

            bool output = false;

            for (int i = 0; i < numberOfTickets; i++)
            {
                CustomerDatabaseEntities context = new CustomerDatabaseEntities();
                CurrentTicket.EventTitle = EventTitle;
                CurrentTicket.Price = LevelOnePrice;
                CurrentTicket.SeatLetter = LevelOneChar;
                CurrentTicket.SeatNumber = i;
                CurrentTicket.EventStart = EventStartLevelOne;
                CurrentTicket.EventEnd = EventEndLevelOne;
                context.Ticket_Details.Add(CurrentTicket);
                context.SaveChanges();
                context.Dispose();
            }

            string levelTwoChar = LevelTwoChar;
            decimal levelTwoPrice = LevelTwoPrice;
            int levelTwoCapacity = LevelTwoCapacity;

            if (levelTwoChar == null || levelTwoPrice == 0 || levelTwoCapacity == 0)
            {
                return true;
            }
            else if (levelTwoCapacity > 0)
            {
                for (int i = 0; i < LevelTwoCapacity; i++)
                {
                    CustomerDatabaseEntities context = new CustomerDatabaseEntities();
                    CurrentTicket.EventTitle = EventTitle;
                    CurrentTicket.Price = LevelTwoPrice;
                    CurrentTicket.SeatLetter = LevelTwoChar;
                    CurrentTicket.SeatNumber = i;
                    CurrentTicket.EventStart = EventStartLevelOne;
                    CurrentTicket.EventEnd = EventEndLevelOne;
                    context.Ticket_Details.Add(CurrentTicket);
                    context.SaveChanges();
                    context.Dispose();
                }
            }

            string levelThreeChar = LevelThreeChar;
            int levelThreeCapacity = LevelThreeCapacity;
            decimal levelThreePrice = LevelThreePrice;
            if (levelThreeChar == null || levelThreePrice == 0 || levelTwoCapacity == 0)
            {
                return true;
            }
            else if (levelTwoCapacity > 0)
            {
                for (int i = 0; i < levelThreeCapacity; i++)
                {
                    CustomerDatabaseEntities context = new CustomerDatabaseEntities();
                    CurrentTicket.EventTitle = EventTitle;
                    CurrentTicket.Price = LevelThreePrice;
                    CurrentTicket.SeatLetter = LevelThreeChar;
                    CurrentTicket.SeatNumber = i;
                    CurrentTicket.EventStart = EventStartLevelOne;
                    CurrentTicket.EventEnd = EventEndLevelOne;
                    context.Ticket_Details.Add(CurrentTicket);
                    context.SaveChanges();
                    context.Dispose();
                }
            }
            output = true;

            return output;
        }

        #endregion

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
