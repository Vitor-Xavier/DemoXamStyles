using DemoXamStyles.ViewModels;
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
            BindingContext = new MainViewModel();
        }
    }
}