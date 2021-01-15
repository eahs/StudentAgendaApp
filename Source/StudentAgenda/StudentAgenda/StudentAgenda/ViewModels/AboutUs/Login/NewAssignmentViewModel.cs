using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using StudentAgenda.Models;
using Xamarin.Forms;

namespace StudentAgenda.ViewModels
{
    public class NewAssignmentViewModel : BaseViewModel
    {
        private string subject;
        private string difficulty;
        private string materials;
        private string time;
        private string date;

        public NewAssignmentViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(subject)
                && !String.IsNullOrWhiteSpace(difficulty)
                && !String.IsNullOrWhiteSpace(materials)
                && !String.IsNullOrWhiteSpace(time)
                && !String.IsNullOrWhiteSpace(date);
        }

        public string Subject
        {
            get => subject;
            set => SetProperty(ref subject, value);
        }

        public string Difficulty
        {
            get => difficulty;
            set => SetProperty(ref difficulty, value);
        }

        public string Materials
        {
            get => materials;
            set => SetProperty(ref materials, value);
        }

        public string Time
        {
            get => time;
            set => SetProperty(ref time, value);
        }

        public string Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Subject = Subject,
                Difficulty = Difficulty,
                Materials = Materials,
                Time = Time,
                Date = Date
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
