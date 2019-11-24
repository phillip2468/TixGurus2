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
                MessageBox.Show("Something wrong");
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
                if (CurrentEvent.Event_Title == null)
                {
                    return CurrentEvent.Event_Title;
                }

                return CurrentEvent.Event_Title;
            }
            set
            {
                CurrentEvent.Event_Title = value;
                OnPropertyChanged($"Event_Title");
            }
        }

        /// <summary>
        /// Property for event description
        /// </summary>
        public string EventDescription
        {
            get
            {
                if (CurrentEvent.Event_Description == null)
                {
                    return CurrentEvent.Event_Description;
                }

                return CurrentEvent.Event_Description;
            }
            set
            {
                CurrentEvent.Event_Description = value;
                OnPropertyChanged($"Event_description");
            }
        }

        /// <summary>
        /// Property for event start
        /// </summary>
        public DateTime EventStart
        {
            get
            {
                if (CurrentEvent.Event_Start == DateTime.MinValue)
                {
                    return CurrentEvent.Event_Start;
                }

                return CurrentEvent.Event_Start;
            }
            set
            {
                CurrentEvent.Event_Start = value;
                OnPropertyChanged($"Event_Start");
            }
        }
        /// <summary>
        /// Property for event end
        /// </summary>
        public DateTime EventEnd
        {
            get
            {
                if (CurrentEvent.Event_End == DateTime.MinValue)
                {
                    return CurrentEvent.Event_End;
                }

                return CurrentEvent.Event_End;
            }
            set
            {
                CurrentEvent.Event_End = value;
                OnPropertyChanged($"Event_End");
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
                if (CurrentEvent.Capacity == 0)
                {
                    return CurrentEvent.Capacity;
                }

                return CurrentEvent.Capacity;
            }
            set
            {
                CurrentEvent.Capacity = value;
                OnPropertyChanged($"Capacity");
            }
        }

        public string EventLocation
        {
            get
            {
                if (CurrentEvent.Event_Location == null)
                {
                    return CurrentEvent.Event_Location;
                }

                return CurrentEvent.Event_Location;
            }
            set
            {
                CurrentEvent.Event_Location = value;
                OnPropertyChanged($"Event_Location");
            }
        }

        public string EventAddress
        {
            get
            {
                if (CurrentEvent.Event_Address == null)
                {
                    return CurrentEvent.Event_Address;
                }

                return CurrentEvent.Event_Address;
            }
            set
            {
                CurrentEvent.Event_Address = value;
                OnPropertyChanged($"Event_Address");
            }
        }


        private string imagePath2;
        /// <summary>
        /// Property for getting the path of the image
        /// of the layout
        /// </summary>
        public string ImagePath2
        {
            get { return imagePath2; }
            set
            {
                imagePath2 = value;
                OnPropertyChanged("ImagePath2");
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
                ImagePath2 = open.FileName;
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
                byte[] imageBytes2 = ConvertImageToByteArray(ImagePath2);
                CurrentEvent.Event_Picture = imageBytes;
                CurrentEvent.Event_Layout = imageBytes2;
                CurrentEvent.Event_Title = EventTitle.Trim();
                CurrentEvent.Event_Description = EventDescription.Trim();
                CurrentEvent.Event_Start = EventStart;
                CurrentEvent.Event_End = EventEnd;
                CurrentEvent.Capacity = Capacity;
                CurrentEvent.Event_Location = EventLocation.Trim();
                CurrentEvent.Event_Address = EventAddress.Trim();
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

