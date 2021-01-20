using System.ComponentModel;
using Xamarin.Forms;
using StudentAgenda.ViewModels;

namespace StudentAgenda.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}