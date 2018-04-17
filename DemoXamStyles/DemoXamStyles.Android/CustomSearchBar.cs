using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DemoXamStyles.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchBar))]
namespace DemoXamStyles.Droid
{
    class CustomSearchBar : SearchBarRenderer
    {
        public CustomSearchBar(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var searchView = base.Control as SearchView;

                //Get the Id of the plate
                var plateId = Resources.GetIdentifier("android:id/search_plate", null, null);
                var plate = Control.FindViewById(plateId);
                plate.SetBackgroundColor(Android.Graphics.Color.Transparent);

                //Get the Id for your search icon
                int searchIconId = Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
                ImageView searchViewIcon = (ImageView)searchView.FindViewById<ImageView>(searchIconId);
                ViewGroup linearLayoutSearchView = (ViewGroup)searchViewIcon.Parent;

                searchViewIcon.SetAdjustViewBounds(true);

                //Remove the search icon from the view group and add it once again to place it at the end of the view group elements
                linearLayoutSearchView.RemoveView(searchViewIcon);
                //linearLayoutSearchView.AddView(searchViewIcon);
            }
        }
    }
}