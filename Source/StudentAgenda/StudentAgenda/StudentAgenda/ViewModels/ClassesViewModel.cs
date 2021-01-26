using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using StudentAgenda.Models;
using StudentAgenda.Views;

namespace StudentAgenda.ViewModels
{
    public class ClassesViewModel : BaseViewModel
    {
        private Class _selectedItem;

        public ObservableCollection<Class> Classes { get; }
        public Command AddClassCommand { get; }
        public Command LoadClassesCommand { get; }
        public Command<Class> ClassTapped { get; }

        public ClassesViewModel()
        {
            Title = "Classes";
            Classes = new ObservableCollection<Class>();

            ClassTapped = new Command<Class>(OnItemSelected);
            AddClassCommand = new Command(OnAddItem);
            
        }

        protected override async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Classes.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Classes.Add(item);
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


        public Class SelectedItem
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


        async void OnItemSelected(Class item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ClassDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}