using System;
using System.Diagnostics;
using System.Threading.Tasks;
using StudentAgenda.Models;
using StudentAgenda.Views;
using Xamarin.Forms;

namespace StudentAgenda.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;

        public ItemDetailViewModel()
        {
            
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private void OnCancel(object obj)
        {
            throw new NotImplementedException();
        }
        
        private void OnSave(object obj)
        {
            throw new NotImplementedException();
        }

        public string Id { get; set; }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public Command CancelCommand { get; }
        
        public Command SaveCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync(nameof(ClassesPage));
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async void OnSave()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            Assignment newItem = new Assignment()
            {
                Id = Guid.NewGuid().ToString(),
                Text = Text,
                Description = Description
            };

        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
