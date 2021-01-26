using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using StudentAgenda.Models;
using StudentAgenda.ViewModels;

namespace StudentAgenda.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Assignment Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}