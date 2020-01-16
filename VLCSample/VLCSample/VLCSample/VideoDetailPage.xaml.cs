using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VLCSample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoDetailPage : ContentPage
    {
        private readonly MainViewModel _viewModel;
        public VideoDetailPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as MainViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
            mediaPlayer.PlaybackControls = _viewModel.PlaybackControls;
            DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.OnDisappearing();
            DeviceDisplay.MainDisplayInfoChanged -= OnMainDisplayInfoChanged;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OnlyVideoPage(_viewModel.MediaPlayer));
        }

        void OnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (e.DisplayInfo.Orientation == DisplayOrientation.Landscape)
                {
                    await Navigation.PushAsync(new OnlyVideoPage(_viewModel.MediaPlayer));
                }
            });
        }
    }
}