using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace DemoXamStyles
{
	public partial class App : Application
	{
		public App ()
		{
            // Initialize Live Reload.
            //LiveReload.Init();

            InitializeComponent();

            MainPage = new Pages.CustomNavigationPage(new Pages.HomePage());
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
