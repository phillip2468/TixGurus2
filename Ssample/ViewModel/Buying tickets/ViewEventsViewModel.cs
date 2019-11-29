using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;
using Ssample.Views.Buying_tickets;
using Syncfusion.UI.Xaml.Grid;

namespace Ssample.ViewModel.Buying_tickets
{
    public class ViewEventsViewModel : NavigationViewModelBase
    {
        private NavigationViewModelBase buyingTicketsViewModel;

        public ICommand NavCommand { get; set; }
        public ICommand NavCommand2 { get; set; }
        public ViewEventsViewModel()
        {
            buyingTicketsViewModel = new PurchaseTicketsViewModel();

            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
            NavCommand2 = new RelayCommand<NavigationViewModelBase>(Nav2);
            Tickets = (from data in context.Ticket_Details where data.eventTitle == Properties.Settings.Default.EventTitle select data).ToList();

        }
        
        private void Nav(NavigationViewModelBase viewModel)
        {
            Properties.Settings.Default.SeatLocation = "";
            Navigate(viewModel);
        }

        private void Nav2(NavigationViewModelBase viewModel)
        {
            if (SaveSeatSelection() != true)
            {
                MessageBox.Show("Not correct seats");
            }
            else
            {
                Navigate(viewModel);
            }
        }

        public List<Ticket_Details> Tickets { get; set; }

        private BitmapImage _image;
        public BitmapImage Image
        {
            get
            {
                _image = LoadImage(LoadImage());
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        /// <summary>
        /// Function which converts imageData to
        /// an actual image
        /// </summary>
        /// <param name="imageData"></param>
        /// <returns></returns>
        public BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        public List<Event_Details> Event { get; set; }

        public byte[] LoadImage()
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            Event = (List<Event_Details>)(from data in context.Event_Details select data).ToList();
            var selectedEvent = Event.Find(x => x.eventTitle == Properties.Settings.Default.EventTitle);
            byte[] selectedImage = selectedEvent.eventPicture;
            return selectedImage;
        }

        private string _seatPlace;
        public string SeatPlace
        {
            get
            {
                if (_seatPlace == null)
                {
                    return _seatPlace;
                }
                return _seatPlace;
            }
            set
            {
                _seatPlace = value;
                OnPropertyChanged($"Seat");
            }
        }

        private List<Ticket_Details> tickets = new List<Ticket_Details>();
        private bool SaveSeatSelection()
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            Tickets = (from data in context.Ticket_Details select data).ToList();
            var selectedEvent = tickets.Find(x => x.eventTitle == Properties.Settings.Default.EventTitle);

            if (SeatPlace == null) 
            {
                return false;
            }

            else
            {
                Properties.Settings.Default.SeatLocation = SeatPlace.Trim();
                return true;
            }
            
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { OnPropertyChanged(ref _isSelected, value); }
        }

    }

    
}
