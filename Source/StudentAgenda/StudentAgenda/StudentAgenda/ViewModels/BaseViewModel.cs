using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StudentAgenda.Helpers;
using Xamarin.Forms;

using StudentAgenda.Models;
using StudentAgenda.Services;
using System.Windows.Input;
using System.Threading.Tasks;

namespace StudentAgenda.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private bool isBusy = false, isError, dataAvailable;
        private string errorMessage = "";
        public IDataStore<Class> DataStore => DependencyService.Get<IDataStore<Class>>();
        public IDataStore<Assignment> AssignmentsDataStore => DependencyService.Get<IDataStore<Assignment>>();

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }


        /// <summary>
        /// Gets or sets if the View Model generated an error
        /// </summary>
        public bool IsError
        {
            get { return isError; }
            set
            {
                SetProperty(ref isError, value);
                OnPropertyChanged("DataNotAvailable");
            }
        }

        /// <summary>
        /// Gets or sets if the View Model data is available
        /// </summary>
        public bool DataAvailable
        {
            get { return dataAvailable; }
            set
            {
                SetProperty(ref dataAvailable, value);
                OnPropertyChanged("DataNotAvailable");
            }
        }

        public bool DataNotAvailable
        {
            get { return !DataAvailable && !IsBusy && !IsError; }
        }

        /// <summary>
        /// Gets or sets current error message if data was unable to be retrieved
        /// </summary>
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }


        private RelayCommand loadItemsCommand;
        public virtual ICommand LoadItemsCommand
        {
            get
            {
                return loadItemsCommand ?? (loadItemsCommand = new RelayCommand(async () => await ExecuteLoadItemsCommand()));
            }
        }

        protected virtual async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy) return;
            IsBusy = true;

            // Make async request to obtain data
            await LoadItemsAsync();

            IsBusy = false;
        }

        // Override this method to load data
        protected virtual async Task LoadItemsAsync()
        {
            await Task.Delay(1);

            // Basic pattern
            /*
            try
            {
                bool success = false;

                // Make async request to obtain data

                if (success)
                {
                    IsError = false;
                    DataAvailable = true;
                }
                else
                {
                    // An error occurred that is stored
                    ErrorMessage = "An error occurred";
                    DataAvailable = false;
                    IsError = true;
                }
            }
            catch (Exception e)
            {
                // An exception occurred
                DataAvailable = false;
            }
            */
        }


        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// The PropertyChanged event occurs when changing the value of property.
        /// </summary>
        /// <param name="propertyName">The PropertyName</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}