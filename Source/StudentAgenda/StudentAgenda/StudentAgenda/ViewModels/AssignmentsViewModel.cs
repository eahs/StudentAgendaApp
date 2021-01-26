using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using StudentAgenda.Models;
using StudentAgenda.Views;

namespace StudentAgenda.ViewModels
{
    public class AssignmentsViewModel : BaseViewModel
    {
        private Assignment _selectedItem;

        public ObservableCollection<Assignment> Assignments { get; }
        public Command AddAssignmentCommand { get; }
        public Command LoadAssignmentsCommand { get; }
        public Command<Assignment> AssignmentTapped { get; }

        public AssignmentsViewModel()
        {
            Title = "Assignments";
            Assignments = new ObservableCollection<Assignment>();
            AssignmentTapped = new Command<Assignment>(OnItemSelected);
            AddAssignmentCommand = new Command(OnAddItem);

            ExecuteLoadItemsCommand();
        }

        protected override async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Assignments.Clear();
                var items = await AssignmentsDataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Assignments.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Assignment SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }


        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }


        async void OnItemSelected(Assignment item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}