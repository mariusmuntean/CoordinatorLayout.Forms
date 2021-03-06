﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("materialdesignicons-webfont.ttf", Alias = "MaterialDesign")]
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace CoordinatorLayout.XamarinForms.Sample
{
    public partial class App : Application
    {
        public App()
        {
            Device.SetFlags(new string[]{ "MediaElement_Experimental" });
            
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
    }
}