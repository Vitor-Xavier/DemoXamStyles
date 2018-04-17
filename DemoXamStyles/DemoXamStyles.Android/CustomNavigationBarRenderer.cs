using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using TCC_App.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using AToolbar = Android.Support.V7.Widget.Toolbar;
using AView = Android.Views.View;


[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationBarRenderer))]
namespace TCC_App.Droid
{
    internal static class MeasureSpecFactory
    {
        public static int GetSize(int measureSpec)
        {
            const int modeMask = 0x3 << 30;
            return measureSpec & ~modeMask;
        }

        // Literally does the same thing as the android code, 1000x faster because no bridge cross
        // benchmarked by calling 1,000,000 times in a loop on actual device
        public static int MakeMeasureSpec(int size, MeasureSpecMode mode)
        {
            return size + (int)mode;
        }
    }

    public class CustomNavigationBarRenderer : NavigationPageRenderer
    {
        AToolbar _toolbar;
        private bool ToolbarVisible;
        private int ContainerPadding;
        private bool isTransparent;
        private int _statusBarHeight;

        public CustomNavigationBarRenderer(Android.Content.Context context) : base(context)
        {
            //_bottomSafeArea = Convert.ToInt32(context.Resources.DisplayMetrics.Density * 12.5);
            _statusBarHeight = Convert.ToInt32(context.Resources.DisplayMetrics.Density * 30);//Alterar
            _statusBarHeight = _statusBarHeight > 56 ? 56 : _statusBarHeight;
            //_tabSafeArea = Convert.ToInt32(context.Resources.DisplayMetrics.Density * Naxam.Controls.Platform.Droid.BottomTabbedRenderer.BottomBarHeight);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);

            var memberInfo = typeof(CustomNavigationBarRenderer).BaseType;
            if (memberInfo != null)
            {
                isTransparent = e.NewElement.BarBackgroundColor.A == 0 ? true : false;

                var propVisible = memberInfo.GetProperty(nameof(ToolbarVisible), BindingFlags.Instance | BindingFlags.NonPublic);
                ToolbarVisible = (bool)propVisible.GetValue(this);

                var propPadding = memberInfo.GetProperty(nameof(ContainerPadding), BindingFlags.Instance | BindingFlags.NonPublic);
                ContainerPadding = (int)propPadding.GetValue(this);

                var field = memberInfo.GetField(nameof(_toolbar), BindingFlags.Instance | BindingFlags.NonPublic);
                _toolbar = field.GetValue(this) as AToolbar;
                _toolbar.SetBackgroundColor(Color.Transparent.ToAndroid());

                Activity context = Context as Activity;
                //context.Window.AddFlags(WindowManagerFlags.Fullscreen);
                context.Window.DecorView.SetFitsSystemWindows(true);

                var window = context.Window;

                window.ClearFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                //window.AddFlags(WindowManagerFlags.TranslucentStatus);

                //window.SetStatusBarColor(Android.Graphics.Color.Rgb(116, 181, 13));
                //window.SetStatusBarColor(Color.Red.ToAndroid());

                //context.Window.ClearFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

                //context.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LayoutStable | (StatusBarVisibility)SystemUiFlags.Fullscreen;

            }
        }

        private int ActionBarHeight()
        {
            int attr = Resource.Attribute.actionBarSize;

            int actionBarHeight;
            using (var tv = new TypedValue())
            {
                actionBarHeight = 0;
                if (Context.Theme.ResolveAttribute(attr, tv, true))
                    actionBarHeight = TypedValue.ComplexToDimensionPixelSize(tv.Data, Resources.DisplayMetrics);
            }

            //if (actionBarHeight <= 0)
            //	return Device.Info.CurrentOrientation.IsPortrait() ? (int)Context.ToPixels(56) : (int)Context.ToPixels(48);

            return actionBarHeight;
        }

        IPageController PageController => Element;


        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            //var isTabbedBottom = this.Element.CurrentPage.GetType().BaseType == typeof(Naxam.Controls.Forms.BottomTabbedPage);
            //var fixTabbedBottomPage = isTabbedBottom ? _tabSafeArea : 0;//95
            //_bottomSafeArea = isTabbedBottom ? 0 : _bottomSafeArea;

            base.OnLayout(changed, l, t, r, b);

            AToolbar bar = _toolbar;
            bar.BringToFront();

            //
            int barHeight = ActionBarHeight();
            int marginTop = isTransparent ? 0 : _statusBarHeight;

            bar.Measure(MeasureSpecFactory.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly), MeasureSpecFactory.MakeMeasureSpec(barHeight, MeasureSpecMode.Exactly));

            var barOffset = ToolbarVisible ? barHeight : 0;
            int containerHeight = b - t - ContainerPadding - marginTop; //101 45

            PageController.ContainerArea = new Rectangle(0, marginTop, Context.FromPixels(r - l), Context.FromPixels(containerHeight));//100)   30 20

            //
            for (var i = 0; i < ChildCount; i++)
            {
                AView child = GetChildAt(i);

                var pageContainer = child.GetType().GetProperty("Child")?.GetValue(child) as IVisualElementRenderer;
                Page childPage = pageContainer?.Element as Page;

                if (childPage == null)
                    return;

                // We need to base the layout of both the child and the bar on the presence of the NavBar on the child Page itself.
                // If we layout the bar based on ToolbarVisible, we get a white bar flashing at the top of the screen.
                // If we layout the child based on ToolbarVisible, we get a white bar flashing at the bottom of the screen.
                bool childHasNavBar = NavigationPage.GetHasNavigationBar(childPage);

                if (childHasNavBar)
                {
                    bar.Layout(0, 0, r - l, barHeight);
                    child.Layout(0, 0, r, b);
                }

            }
        }
    }
}