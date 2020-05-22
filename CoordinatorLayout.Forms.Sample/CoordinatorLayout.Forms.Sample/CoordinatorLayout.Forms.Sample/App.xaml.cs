using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace CoordinatorLayout.XamarinForms.Sample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static SwipeView GetSwipeView(int itemId)
        {
            var rand = new Random();
            var boxView = new Button
            {
                BackgroundColor = new Color(rand.NextDouble(), rand.NextDouble(), rand.NextDouble()).WithLuminosity(0.8).WithSaturation(0.8),
                Text=itemId.ToString(),
                Command = new Command<int>(OnItemClicked),
                CommandParameter = itemId
            };
            var swipeView = new SwipeView()
            {
                Content = boxView,
                LeftItems = new SwipeItems(new List<SwipeItem> { new SwipeItem() { Text = $"Left {itemId}", Command = new Command<int>(OnLeftSwipeItemClicked), CommandParameter=itemId } }),
                RightItems = new SwipeItems(new List<SwipeItem> { new SwipeItem() { Text = $"Right {itemId}" } })

            };
            return swipeView;
        }

        private static void OnLeftSwipeItemClicked(int itemId)
        {
            Debug.WriteLine(itemId + "Left swipe Clicked");
        }

        private static void OnItemClicked(int itemId)
        {
            Debug.WriteLine(itemId + "Button Clicked");
        }
    }
}