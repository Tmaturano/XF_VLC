using LibVLCSharp.Shared;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VLCSample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OnlyVideoPage : ContentPage
    {
        public OnlyVideoPage(MediaPlayer mediaPlayer)
        {
            InitializeComponent();

            var viewModel = BindingContext as OnlyVideoViewModel;
            viewModel.PreviousMediaPlayer = mediaPlayer;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((OnlyVideoViewModel)BindingContext).OnAppearing();
            DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((OnlyVideoViewModel)BindingContext).OnDisappearing();
            DeviceDisplay.MainDisplayInfoChanged -= OnMainDisplayInfoChanged;
        }

        void OnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (e.DisplayInfo.Orientation == DisplayOrientation.Portrait)
                {
                    await Navigation.PopAsync();
                }
            });
        }
    }
}