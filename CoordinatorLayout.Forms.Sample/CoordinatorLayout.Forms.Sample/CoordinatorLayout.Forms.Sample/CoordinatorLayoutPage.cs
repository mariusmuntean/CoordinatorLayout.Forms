using System;
using Xamarin.Forms;

namespace CoordinatorLayout.XamarinForms.Sample
{
    public class CoordinatorLayoutPage : ContentPage
    {
        public CoordinatorLayoutPage()
        {
            Content = new CoordinatorLayout
            {
                TopView = new BoxView {Color = Color.DodgerBlue, Margin = new Thickness(5)},
                BottomView = GetBottomView(),
                ActionView = GetActionView()
            };
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

            for (var i = 0; i < 15; i++)
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