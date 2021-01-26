using System.ComponentModel;
using Xamarin.Forms;
using StudentAgenda.ViewModels;

namespace StudentAgenda.Views
{
    public partial class ClassDetailPage : ContentPage
    {
        public ClassDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}