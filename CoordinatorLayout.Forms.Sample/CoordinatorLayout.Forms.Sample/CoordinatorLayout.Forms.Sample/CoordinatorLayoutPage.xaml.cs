using System;
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
        }

        private void PopulateBottomView()
        {
            var rand = new Random();

            for (var i = 0; i < 55; i++)
            {
                _stackLayout.Children.Add(new BoxView
                {
                    Color = new Color(rand.NextDouble(), rand.NextDouble(), rand.NextDouble()).WithLuminosity(0.8).WithSaturation(0.8),
                    InputTransparent = true
                });
            }
        }
    }
}