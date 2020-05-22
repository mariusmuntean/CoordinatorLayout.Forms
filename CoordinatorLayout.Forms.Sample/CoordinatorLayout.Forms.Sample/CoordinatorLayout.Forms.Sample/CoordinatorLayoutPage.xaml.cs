using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CoordinatorLayout.XamarinForms.Sample
{
    public partial class CoordinatorLayoutPage : ContentPage
    {
        public CoordinatorLayoutPage()
        {
            InitializeComponent();
            PopulateBottomView();
        }

        private void OnCoordinatorLayoutOnScrollEventHandler(object sender, ScrollEventArgs args)
        {
        }

        private void OnCoordinatorLayoutOnExpansionEventHandler(object sender, ExpansionEventArgs args)
        {
            _image.Opacity = args.Progress;
            _boxView.Opacity = 1.0 - args.Progress;
        }

        private void PopulateBottomView()
        {
            for (var i = 0; i < 55; i++)
            {
                _stackLayout.Children.Add(App.GetSwipeView(i));
            }
        }
    }
}