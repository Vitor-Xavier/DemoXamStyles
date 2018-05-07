using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoXamStyles.ViewModels
{
    public class SearchViewModel : BindableObject
    {
        public ICommand CloseCommand => new Command(CloseCommandExecute);

        private INavigation _navigation;

        private string _searchTerm;

        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;
                MessagingCenter.Send(SearchTerm, "SearchTerm");
                OnPropertyChanged();
            }
        }


        public SearchViewModel(INavigation navigation, string searchTerm = "")
        {
            _navigation = navigation;
            _searchTerm = searchTerm;
        }

        private async void CloseCommandExecute()
        {
            await _navigation.PopAllPopupAsync();
        }
    }
}
