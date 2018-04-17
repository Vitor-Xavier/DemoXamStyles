using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoXamStyles.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoXamStyles.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : ContentPage
	{
		public SearchPage ()
		{
			InitializeComponent ();
            BindingContext = new SearchViewModel();
		}
	}
}