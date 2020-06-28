using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoordinatorLayout.XamarinForms.Sample.Blinkist
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BlinkistPage : ContentPage
    {
        private const float MinCornerRadius = 0.0f;
        private const float MaxCornerRadius = 20.0f;

        private const float MinHorizontalMargin = 0.0f;
        private const float MaxHorizontalMargin = 15.0f;

        public BlinkistPage()
        {
            InitializeComponent();
        }

        private void OnCoordinatorLayoutOnExpansionEventHandler(object sender, ExpansionEventArgs e)
        {
            if (ActionFrame != null)
            {
                ActionFrame.CornerRadius = (float) Math.Max(0.0, MinCornerRadius + e.Progress * MaxCornerRadius);

                var newHorizontalMargin = MinHorizontalMargin + e.Progress * MaxHorizontalMargin;
                ActionFrame.Margin = new Thickness(newHorizontalMargin, ActionFrame.Margin.VerticalThickness);
            }

            if (BackgroundImage != null)
            {
                BackgroundImage.Opacity = e.Progress;
            }

            if (MainTitle != null)
            {
                MainTitle.Opacity = e.Progress;
            }

            if (SecondaryTitle != null)
            {
                if (e.Progress > 0.2f)
                {
                    SecondaryTitle.Opacity = 0.0f;
                }
                else
                {
                    SecondaryTitle.Opacity = (0.2f - e.Progress) / 0.2f;
                }
            }
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