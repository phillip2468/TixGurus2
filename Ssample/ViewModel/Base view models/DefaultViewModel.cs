using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Design;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Ssample.Model;
using Ssample.ViewModel.Buying_tickets;
using Syncfusion.Windows.Shared;

namespace Ssample.ViewModel
{
    /// <summary>
    /// The default view model
    /// </summary>
    public class DefaultViewModel : NavigationViewModelBase
    {
        #region Fields
        private NavigationViewModelBase registerViewModel;

        private NavigationViewModelBase signInViewModel;

        private NavigationViewModelBase viewEventsViewModel;
        #endregion

        #region Commands
        /// <summary>
        /// Icommand interface for switching between
        /// user controls
        /// </summary>
        public ICommand NavCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public DefaultViewModel()
        {
            //Initialisation
            registerViewModel = new RegisterViewModel();
            viewEventsViewModel = new ViewEventsViewModel();
            signInViewModel = new SignInViewModel();

            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);

        }
        #endregion

        #region Helper Functions

        //Navigates to the register view
        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }
        #endregion

        public List<Event_Details> Event { get; set; }

        #region Properties for location 1

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

        public byte[] LoadImage()
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            Event = (List<Event_Details>)(from data in context.Event_Details select data).ToList();
            var selectedEvent = Event.Find(x => x.Event_Title == "The Opera with Snakes");
            byte[] selectedImage = selectedEvent.Event_Picture;
            return selectedImage;
        }

        private string _eventName;
        public string EventName
        {
            get
            {
                _eventName = LoadEventName();
                return _eventName;
            }
            set
            {
                _eventName = value;
                OnPropertyChanged("EventName");
            }
        }

        public string LoadEventName()
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            Event = (List<Event_Details>)(from data in context.Event_Details select data).ToList();
            var selectedEvent = Event.Find(x => x.Event_Title == "The Opera with Snakes");
            string selectedEventName = selectedEvent.Event_Title;
            return selectedEventName;
        }

        private string _eventStart;
        public string EventStart
        {
            get
            {
                _eventStart = LoadEventDate();
                return _eventStart;
            }
            set
            {
                _eventStart = value;
                OnPropertyChanged("EventStart");
            }
        }

        public string LoadEventDate()
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            Event = (List<Event_Details>)(from data in context.Event_Details select data).ToList();
            var selectedEvent = Event.Find(x => x.Event_Title == "The Opera with Snakes");
            string selectedStart = selectedEvent.Event_Start.ToShortDateString();
            return selectedStart;
        }

        #endregion
    }
}
