using System;
using CoordinatorLayout.XamarinForms.Sample.Blinkist;
using Xamarin.Forms;

namespace CoordinatorLayout.XamarinForms.Sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new CoordinatorLayoutPage(), true);
        }

        private void Blinkist_OnClicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new BlinkistPage(), true);
        }
    }
}