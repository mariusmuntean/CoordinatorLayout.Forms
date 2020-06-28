using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoordinatorLayout.XamarinForms.Sample.Blinkist
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BlinkistPage : ContentPage
    {
        public BlinkistPage()
        {
            InitializeComponent();
        }

        private void OnCoordinatorLayoutOnExpansionEventHandler(object sender, ExpansionEventArgs e)
        {
            
            
        }

        private void OnCoordinatorLayoutOnScrollEventHandler(object sender, ScrollEventArgs e)
        {
            
            
        }

        private async void ImageButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }
    }
}