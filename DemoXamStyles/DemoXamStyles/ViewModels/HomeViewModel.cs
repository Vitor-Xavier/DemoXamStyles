using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoXamStyles.ViewModels
{
    public class HomeViewModel : BindableObject
    {
        public ICommand TabCommand => new Command(TabCommandExecute);

        public ICommand SearchCommand => new Command(SearchCommandExecute);

        public ICommand AnimationCommand => new Command(AnimationCommandExecute);

        private bool titleEnabled;

        public bool TitleEnabled
        {
            get { return titleEnabled; }
            set
            {
                titleEnabled = value;
                OnPropertyChanged();
            }
        }


        public async void TabCommandExecute()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Pages.MainPage());
        }

        public async void SearchCommandExecute()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Pages.SearchPage());
        }

        public void AnimationCommandExecute()
        {
            TitleEnabled = true;
        }
    }
}
