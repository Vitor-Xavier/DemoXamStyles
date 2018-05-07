using DemoXamStyles.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoXamStyles.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : PopupPage
	{
		public SearchPage (string searchTerm = "")
		{
			InitializeComponent ();
            BindingContext = new SearchViewModel(Navigation, searchTerm);
		}

        protected override bool OnBackgroundClicked()
        {
            (BindingContext as SearchViewModel).CloseCommand.Execute(this);

            return false;
        }
    }
}