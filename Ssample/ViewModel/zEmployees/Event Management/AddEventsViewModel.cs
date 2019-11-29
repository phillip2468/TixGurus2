using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;

namespace Ssample.ViewModel.zEmployees.Event_Management
{
    public class AddEventsViewModel : NavigationViewModelBase
    {
        private NavigationViewModelBase GenerateTicketsViewModel;
        public ICommand NavCommand { get; set; }

        public ICommand Nav2Command { get; set; }

        public AddEventsViewModel()
        {
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
            Nav2Command = new RelayCommand<NavigationViewModelBase>(Nav2);
        }

        #region Hepler functions

        private void Nav2(NavigationViewModelBase viewModel)
        {
            if (GoToSuccessful())
            {
                MessageBox.Show("The event has been added successfully");
                Navigate(viewModel);
            }
            else
            {
                MessageBox.Show("You entered something wrong");
            }
        }

        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        /// <summary>
        /// Determines if event has been
        /// added successfully
        /// </summary>
        /// <returns></returns>
        private bool GoToSuccessful()
        {
            if (SaveCustomerChanges() != true)
            {
                return false;
            }

            return true;
        }

        #endregion

        /// <summary>
        /// Edit the current event
        /// </summary>
        public Event_Details CurrentEvent { get; set; } = new Event_Details();

        #region Properties

        public string EventTitle
        {
            get
            {
                if (CurrentEvent.eventTitle == null)
                {
                    return CurrentEvent.eventTitle;
                }

                return CurrentEvent.eventTitle;
            }
            set
            {
                CurrentEvent.eventTitle = value;
                OnPropertyChanged($"EventTitle");
            }
        }

        /// <summary>
        /// Property for event description
        /// </summary>
        public string EventDescription
        {
            get
            {
                if (CurrentEvent.eventDescription == null)
                {
                    return CurrentEvent.eventDescription;
                }

                return CurrentEvent.eventDescription;
            }
            set
            {
                CurrentEvent.eventDescription = value;
                OnPropertyChanged($"EventDescription");
            }
        }

        /// <summary>
        /// Property for event start
        /// </summary>
        public DateTime EventStart
        {
            get
            {
                if (CurrentEvent.eventStart == DateTime.MinValue)
                {
                    return CurrentEvent.eventStart;
                }

                return CurrentEvent.eventStart;
            }
            set
            {
                CurrentEvent.eventStart = value;
                OnPropertyChanged($"EventStart");
            }
        }
        /// <summary>
        /// Property for event end
        /// </summary>
        public DateTime EventEnd
        {
            get
            {
                if (CurrentEvent.eventEnd == DateTime.MinValue)
                {
                    return CurrentEvent.eventEnd;
                }

                return CurrentEvent.eventEnd;
            }
            set
            {
                CurrentEvent.eventEnd = value;
                OnPropertyChanged($"EventEnd");
            }
        }

        private string imagePath;
        /// <summary>
        /// Property for getting the path of the image
        /// </summary>
        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }

        ICommand _loadImageCommand;
        /// <summary>
        /// Command to open the file dialog
        /// </summary>
        public ICommand LoadImageCommand
        {
            get
            {
                if (_loadImageCommand == null)
                {
                    _loadImageCommand = new RelayCommand<NavigationViewModelBase>(param => LoadImage());
                }
                return _loadImageCommand;
            }
        }
        /// <summary>
        /// Helper function which opens the file
        /// dialog function
        /// </summary>
        private void LoadImage()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = (".png");
            open.Filter = "Pictures (*.jpg;*.gif;*.png)|*.jpg;*.gif;*.png";

            if (open.ShowDialog() == true)
                ImagePath = open.FileName;
        }

        /// <summary>
        /// Converts string path into a byte array
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public byte[] ConvertImageToByteArray(string imagePath)
        {
            byte[] imageByteArray = null;
            FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                imageByteArray = new byte[reader.BaseStream.Length];
                for (int i = 0; i < reader.BaseStream.Length; i++)
                {
                    imageByteArray[i] = reader.ReadByte();
                }

                return imageByteArray;
            }
        }

        public int Capacity
        {
            get
            {
                if (CurrentEvent.capacity == 0)
                {
                    return CurrentEvent.capacity;
                }

                return CurrentEvent.capacity;
            }
            set
            {
                CurrentEvent.capacity = value;
                OnPropertyChanged($"Capacity");
            }
        }

        public string EventLocation
        {
            get
            {
                if (CurrentEvent.eventLocation == null)
                {
                    return CurrentEvent.eventLocation;
                }

                return CurrentEvent.eventLocation;
            }
            set
            {
                CurrentEvent.eventLocation = value;
                OnPropertyChanged($"EventLocation");
            }
        }

        public string EventAddress
        {
            get
            {
                if (CurrentEvent.eventAddress == null)
                {
                    return CurrentEvent.eventAddress;
                }

                return CurrentEvent.eventAddress;
            }
            set
            {
                CurrentEvent.eventAddress = value;
                OnPropertyChanged($"EventAddress");
            }
        }

        public string EventGenre
        {
            get
            {
                if (CurrentEvent.eventGenre == null)
                {
                    return CurrentEvent.eventGenre;
                }

                return CurrentEvent.eventGenre;
            }
            set
            {
                CurrentEvent.eventGenre = value;
                OnPropertyChanged($"EventGenre");
            }
        }


        private string _imageOfLayout;
        /// <summary>
        /// Property for getting the path of the image
        /// of the layout
        /// </summary>
        public string ImageOfLayout
        {
            get { return _imageOfLayout; }
            set
            {
                _imageOfLayout = value;
                OnPropertyChanged($"ImageofLayout");
            }
        }

        ICommand _loadImageCommand2;
        /// <summary>
        /// Command to open the file dialog
        /// </summary>
        public ICommand LoadImageCommand2
        {
            get
            {
                if (_loadImageCommand2 == null)
                {
                    _loadImageCommand2 = new RelayCommand<NavigationViewModelBase>(param => LoadImage2());
                }
                return _loadImageCommand2;
            }
        }

        private void LoadImage2()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = (".png");
            open.Filter = "Pictures (*.jpg;*.gif;*.png)|*.jpg;*.gif;*.png";

            if (open.ShowDialog() == true)
                ImageOfLayout = open.FileName;
        }

        private bool? _showOnHomePage;
        public bool? ShowOnHomePage
        {
            get { return _showOnHomePage; }
            set
            {
                _showOnHomePage = value;
                _showOnHomePage = (_showOnHomePage != null) ? value : false;
                OnPropertyChanged($"ShowOnHomePage");
            }
        }

        #endregion

        #region Save to database
        private bool SaveCustomerChanges()
        {
            //Initialisation of bool
            bool output = false;

            //Assigns the value of the helper class to the class
            bool isNull = ArePropertiesNotNull(CurrentEvent);

            //Assign a context value
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();

            if (!isNull) 
            {
                byte[] imageBytes = ConvertImageToByteArray(ImagePath);
                byte[] imageBytes2 = ConvertImageToByteArray(ImageOfLayout);
                CurrentEvent.eventPicture = imageBytes;
                CurrentEvent.eventLayout = imageBytes2;
                CurrentEvent.eventGenre = EventGenre.Trim();
                CurrentEvent.eventTitle = EventTitle.Trim();
                CurrentEvent.showOnHomePage = ShowOnHomePage.Value;
                CurrentEvent.eventDescription = EventDescription.Trim();
                CurrentEvent.eventStart = EventStart;
                CurrentEvent.eventEnd = EventEnd;
                CurrentEvent.capacity = Capacity;
                CurrentEvent.eventLocation = EventLocation.Trim();
                CurrentEvent.eventAddress = EventAddress.Trim();
                context.Event_Details.Add(CurrentEvent);

                context.SaveChanges();
                output = true;
                return output;
            }
            return output;
        }
        #endregion

        #region Checking for null
        /// <summary>
        /// A function which checks whether the select object is
        /// null or not null
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="obj">The object itself</param>
        /// <returns>A boolean value</returns>
        public bool ArePropertiesNotNull<T>(T obj)
        {
            return typeof(T).GetProperties().All(propertyInfo => propertyInfo.GetValue(obj) != null);
        }

        #endregion
    }
}

