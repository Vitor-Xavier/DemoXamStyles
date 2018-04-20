using DemoXamStyles.Models;
using DemoXamStyles.ViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoXamStyles.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel(Navigation);
        }

        public async void ItemSelected(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item;
            (sender as ListView).SelectedItem = null;

            await Navigation.PushPopupAsync(new DetailsPage(item as Character));
        }
    }
}