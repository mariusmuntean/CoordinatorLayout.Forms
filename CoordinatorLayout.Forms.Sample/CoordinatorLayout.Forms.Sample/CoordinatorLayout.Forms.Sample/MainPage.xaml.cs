using System;
using Xamarin.Forms;

namespace CoordinatorLayout.XamarinForms.Sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            _stackLayout.Children.Add(App.GetSwipeView(1));
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new CoordinatorLayoutPage(), true);
        }
    }
}