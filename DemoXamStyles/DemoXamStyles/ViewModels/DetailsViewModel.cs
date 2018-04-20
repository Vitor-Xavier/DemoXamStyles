using DemoXamStyles.Models;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoXamStyles.ViewModels
{
    public class DetailsViewModel : BindableObject
    {
        private INavigation _navigation;

        public ICommand CloseCommand => new Command(CloseCommandExecute);

        private Character character;

        public Character Character
        {
            get { return character; }
            set
            {
                character = value;
                OnPropertyChanged();
            }
        }

        public DetailsViewModel(INavigation navigation, Character character)
        {
            _navigation = navigation;
            Character = character;
        }

        private async void CloseCommandExecute()
        {
            await _navigation.PopAllPopupAsync();
        }
    }
}
