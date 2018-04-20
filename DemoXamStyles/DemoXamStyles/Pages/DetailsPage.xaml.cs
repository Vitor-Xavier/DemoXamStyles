using DemoXamStyles.Models;
using DemoXamStyles.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoXamStyles.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailsPage : PopupPage
	{
        public DetailsPage(Character character)
		{
			InitializeComponent ();
            BindingContext = new DetailsViewModel(Navigation, character);
		}

        protected override bool OnBackgroundClicked()
        {
            (BindingContext as DetailsViewModel).CloseCommand.Execute(this);

            return false;
        }

    }
}