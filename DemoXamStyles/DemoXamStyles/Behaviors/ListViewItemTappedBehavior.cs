using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoXamStyles.Behaviors
{
    public class ListViewItemTappedBehavior : Behavior<ListView>
    {
        public static readonly BindableProperty ItemTappedCommandProperty =
                BindableProperty.Create("ItemTappedCommand", typeof(ICommand), typeof(ListViewItemTappedBehavior), null);

        public ListView AssociatedObject { get; private set; }

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            bindable.BindingContextChanged += OnBindingContextChanged;
            bindable.ItemSelected += OnListViewItemTapped;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            bindable.ItemSelected -= OnListViewItemTapped;
            AssociatedObject = null;
        }

        public ICommand ItemTappedCommand
        {
            get { return (ICommand)GetValue(ItemTappedCommandProperty); }
            set { SetValue(ItemTappedCommandProperty, value); }
        }

        void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        void OnListViewItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
            if (ItemTappedCommand == null)
            {
                return;
            }

            if (ItemTappedCommand.CanExecute(e.SelectedItem))
            {
                ItemTappedCommand.Execute(e.SelectedItem);
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }

    }
}
