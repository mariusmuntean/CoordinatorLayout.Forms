using System;
using Xamarin.Forms;

namespace CoordinatorLayout.XamarinForms.Sample
{
    public class CoordinatorLayoutPage : ContentPage
    {
        private Label _expansionLbl;
        private Label _scrollLbl;

        public CoordinatorLayoutPage()
        {
            var rootLayout = new Grid();
            var coordinatorLayout = new CoordinatorLayout
            {
                TopView = new BoxView {Color = Color.DodgerBlue, Margin = new Thickness(5)},
                BottomView = GetBottomView(),
                ActionView = GetActionView(),
            };
            coordinatorLayout.ExpansionEventHandler += (sender, args) => _expansionLbl.Text = $"Expansion: {args.Progress}";
            coordinatorLayout.ScrollEventHandler += (sender, args) => _scrollLbl.Text = $"Scroll {args.Progress}";
            coordinatorLayout.ProportionalTopViewHeightMax = 0.33d;
            coordinatorLayout.ProportionalTopViewHeightMin = 0.1d;
            coordinatorLayout.ProportionalSnapHeight = 0.3d;
            coordinatorLayout.ShouldSnap = true;
            coordinatorLayout.ShouldKineticScroll = true;
            coordinatorLayout.ShouldExpandFromKineticScroll = true;
            rootLayout.Children.Add(coordinatorLayout);

            _expansionLbl = new Label
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Start,
                Margin = new Thickness(5)
            };
            rootLayout.Children.Add(_expansionLbl);

            _scrollLbl = new Label
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(5)
            };
            rootLayout.Children.Add(_scrollLbl);

            Content = rootLayout;
        }

        private View GetActionView()
        {
            return new Button
            {
                Text = "Hi",
                TextColor = Color.Goldenrod,
                FontSize = 18,
                BorderColor = Color.Goldenrod,
                BorderWidth = 2.0,
                HeightRequest = 50,
                WidthRequest = 50,
                CornerRadius = 25,
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(15)
            };
        }

        private static StackLayout GetBottomView()
        {
            var rand = new Random();
            var stackLayout = new StackLayout
            {
                InputTransparent = true,
                CascadeInputTransparent = true
            };

            for (var i = 0; i < 55; i++)
            {
                stackLayout.Children.Add(new BoxView
                {
                    Color = new Color(rand.NextDouble(), rand.NextDouble(), rand.NextDouble()).WithLuminosity(0.8).WithSaturation(0.8),
                    InputTransparent = true
                });
            }

            return stackLayout;
        }
    }
}