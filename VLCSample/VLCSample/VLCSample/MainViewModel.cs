using LibVLCSharp.Shared;
using System.ComponentModel;

namespace VLCSample
{
    public class MainViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
        }

        private LibVLC _libVLC;
        /// <summary>
        /// Gets the <see cref="LibVLCSharp.Shared.LibVLC"/> instance.
        /// </summary>
        public LibVLC LibVLC
        {
            get => _libVLC;
            private set => Set(nameof(LibVLC), ref _libVLC, value);
        }

        private MediaPlayer _mediaPlayer;
        /// <summary>
        /// Gets the <see cref="LibVLCSharp.Shared.MediaPlayer"/> instance.
        /// </summary>
        public MediaPlayer MediaPlayer
        {
            get => _mediaPlayer;
            private set => Set(nameof(MediaPlayer), ref _mediaPlayer, value);
        }

        /// <summary>
        /// Initialize LibVLC and playback when page appears
        /// </summary>
        public void OnAppearing()
        {
            Core.Initialize();

            if (LibVLC is null)
            {
                LibVLC = new LibVLC();

                var media = new Media(LibVLC,
                    "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4",
                    FromType.FromLocation);


                //https://code.videolan.org/videolan/LibVLCSharp/blob/3.x/LibVLCSharp/Shared/MediaPlayerElement/AspectRatioManager.cs
                if (MediaPlayer is null)
                { //Scale 2 for fit screen
                    MediaPlayer = new MediaPlayer(media) { EnableHardwareDecoding = true, AspectRatio = null, Scale = 0, Fullscreen = true };
                    MediaPlayer.Play();
                }
            }
        }

        public void OnDisappearing()
        {
            if (MediaPlayer.IsPlaying)
            {
                MediaPlayer.Pause();
            }

            LibVLC.Dispose();
        }

        //With Prism, we can get the time, position and Media of the Full screen (OnlyVideoViewModel) MediaPlayer and set the MediaPlayer of this ViewModel, in the OnNavigatedTo method

        private void Set<T>(string propertyName, ref T field, T value)
        {
            if (field == null && value != null || field != null && !field.Equals(value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
