using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using StudentAgenda.Models;
using StudentAgenda.Views;
using StudentAgenda.Services;

namespace StudentAgenda.ViewModels
{
    public class AssignmentViewModel : BaseViewModel
    {
        private Assignment _selectedAssignment;

        public ObservableCollection<Assignment> Assignments { get; }
        public Command AddAssignmentCommand { get; }
        public Command<Assignment> AssignmentTapped { get; }

        public AssignmentViewModel()
        {
            Title = "Assignments";
            Assignments = new ObservableCollection<Assignment>();

            AssignmentTapped = new Command<Assignment>(OnAssignmentSelected);

            AddAssignmentCommand = new Command(OnAddAssignment);
        }

       

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedAssignment = null;
        }

        public Assignment SelectedAssignment
        {
            get => _selectedAssignment;
            set
            {
                SetProperty(ref _selectedAssignment, value);
                OnAssignmentSelected(value);
            }
        }

        public string NewAssignmentPage { get; private set; }

        private async void OnAddAssignment(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewAssignmentPage));
        }


        async void OnAssignmentSelected(Assignment assignment)
        {
            if (assignment == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(AssignDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={assignment.Id}");
        }
    }
}