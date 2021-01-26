using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using StudentAgenda.Models;
using StudentAgenda.Views;
using StudentAgenda.ViewModels;

namespace StudentAgenda.Views
{
    public partial class ClassesPage : ContentPage
    {
        ClassesViewModel _viewModel;

        public ClassesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ClassesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}