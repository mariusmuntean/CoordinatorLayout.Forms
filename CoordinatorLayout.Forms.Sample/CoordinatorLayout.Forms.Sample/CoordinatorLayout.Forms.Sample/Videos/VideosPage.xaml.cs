using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoordinatorLayout.XamarinForms.Sample.Videos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideosPage : ContentPage
    {
        private readonly Video[] _images = {
            new Video("Xamarin.Forms Navigation",
                "https://sec.ch9.ms/ch9/8e8a/0f87619e-0898-44f1-917c-b37629508e8a/Xamarin101XamarinFormsinCSharp_high.mp4",
                "https://sec.ch9.ms/ch9/6b33/e3d4297a-1584-4a91-94b8-ac0ed9826b33/Xamarin101XamarinFormsinCSharp_512.jpg"),
            new Video("Xamarin.Forms MVVM with XAML", 
                "https://sec.ch9.ms/ch9/3901/c6e0e4e6-bb93-4033-a484-040a874f3901/Xamarin101XamarinFormsMVVMXAML_high.mp4",
                "https://sec.ch9.ms/ch9/4cb2/7b00aec5-d1bf-4f4f-9a99-7ddb96a64cb2/Xamarin101XamarinFormsMVVMXAML_512.jpg"),
            new Video("XAML Hot Reload", 
                "https://sec.ch9.ms/ch9/6d54/bbc3e13c-67e9-4a06-92ae-55555b7a6d54/XamarinShowXAMLHotReload_high.mp4",
                "https://sec.ch9.ms/ch9/e7cf/93ee4ca3-ed48-4768-bb3f-42f6cc95e7cf/XamarinShowXAMLHotReload_512.jpg"),
            new Video("Design-time data",
                "https://sec.ch9.ms/ch9/df33/b62caeeb-3a54-4a94-bc84-1ebe9314df33/DesignTimeData_high.mp4",
                "https://sec.ch9.ms/ch9/2068/b831d788-9c88-4ec9-86d2-2c7f7c322068/DesignTimeData_512.jpg"),
            new Video("Single Page UI", 
                "https://sec.ch9.ms/ch9/3a53/2814d0eb-8594-4197-bac3-70cc3e7d3a53/Xamarin101SinglePageUICsharp_high.mp4",
                "https://sec.ch9.ms/ch9/4ed7/b2d39e0b-c7ba-48d7-a578-468c89894ed7/Xamarin101SinglePageUICsharp_512.jpg"),
            new Video("OnIdiom",
                "https://sec.ch9.ms/ch9/e1b6/f17a0836-e705-4a1b-a808-281b0279e1b6/OnIdiom_high.mp4",
                "https://sec.ch9.ms/ch9/4c05/da5f5afa-1c4b-465d-8e02-5311a52f4c05/OnIdiom_512.jpg"),
            new Video("Clipboard", 
                "https://sec.ch9.ms/ch9/c75c/52d20da5-29cf-484e-b181-f26b8d29c75c/Xamarin_Clipboard_high.mp4",
                "https://sec.ch9.ms/ch9/c75c/52d20da5-29cf-484e-b181-f26b8d29c75c/Xamarin_Clipboard_512.jpg"),
            new Video("Secure Storage",
                "https://sec.ch9.ms/ch9/45dd/f823a73f-0b3a-47fc-9627-9bed25a745dd/Xamarin_SecureStorage_high.mp4",
                "https://sec.ch9.ms/ch9/45dd/f823a73f-0b3a-47fc-9627-9bed25a745dd/Xamarin_SecureStorage_512.jpg"),
        };

        public VideosPage()
        {
            InitializeComponent();

            collectionView.ItemsSource = _images;
            collectionView.SelectedItem = _images.First();
            mediaElement.Source = _images.First().Source;
        }

        protected override void OnDisappearing()
        {
            if (mediaElement.CurrentState == MediaElementState.Playing)
            {
                mediaElement.Stop();
            }

            base.OnDisappearing();
        }

        private void CollectionView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.SingleOrDefault() is Video selectedVideo)
            {
                mediaElement.Source = selectedVideo.Source;
            }
        }

        private void OnCoordinatorLayoutOnExpansionEventHandler(object sender, ExpansionEventArgs e)
        {
        }

        private void OnCoordinatorLayoutOnScrollEventHandler(object sender, ScrollEventArgs e)
        {
        }
    }
}